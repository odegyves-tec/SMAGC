Shader "SMAGC/Unlit"
{
    Properties
    {
        _MainColor("Main Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags {"RenderType" = "Opaque"}

        Pass
        {
            Name "SMAGC-Unlit"

            HLSLPROGRAM

            #pragma vertex UnlitVertexShader
            #pragma fragment FragmentShader

            float4 _MainColor;

            struct Attributes
            {
                float4 positionOS : POSITION;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            Varyings UnlitVertexShader(Attributes input)
            {
                Varyings output = (Varyings)0;
                output.positionCS = input.positionOS;
                return output;
            }

            float4 FragmentShader(Varyings input) : SV_Target
            {
                return _MainColor;
            }

            ENDHLSL
        }
    }
}