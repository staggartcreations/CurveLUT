using UnityEngine;

namespace CurveLUTGenerator
{
    public class CurveLUT
    {
        //Resolutions lower than 256 will start to show a grey line at the bottom
        //High resolutions aren't needed thanks to bilinear interpolation between texels
        private const int resolution = 256;
        
        public static Texture2D Create(AnimationCurve curveA, AnimationCurve curveB, AnimationCurve curveC, AnimationCurve curveD, TextureWrapMode wrapMode = TextureWrapMode.Clamp)
        {
            //32-bit precision since curve values can exceed 1
            Texture2D LUTTex = new Texture2D(resolution, 8, TextureFormat.RGBAFloat, false, true)
            {
                name = "LUT",
                anisoLevel = 0,
                filterMode = FilterMode.Bilinear,
                wrapMode = wrapMode
            };

            for (int x = 0; x < resolution; x++)
            {
                //0-1 value, distance from left to right
                float t = (float)x / (float)resolution;
                
                Color gradientPixel = new Color(
                    (curveA != null ? curveA.Evaluate(t) : 0f),
                    (curveB != null ? curveB.Evaluate(t) : 0f),
                    (curveC != null ? curveC.Evaluate(t) : 0f),
                    (curveD != null ? curveD.Evaluate(t) : 0f)
                    );

                for (int y = 0; y < resolution; y++)
                {
                    LUTTex.SetPixel(x, y, gradientPixel);
                }
            }

            LUTTex.Apply();

            return LUTTex;
        }
    }
}