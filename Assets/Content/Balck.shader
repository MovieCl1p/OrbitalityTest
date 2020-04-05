Shader "Custom/BalckHole"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Position ("Position", Float) = 0
        _Ratio ("Ratio", Float) = 0
        _Rad ("Radius", Float) = 0
        _Distance ("Distance", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
            };

            uniform sampler2D _MainTex;
            uniform float2 _Position;
            uniform float _Rad;
            uniform float _Ratio;
            uniform float _Distance;

            v2f vert (appdata_img v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 offset = i.uv - _Position;
                float2 ration = {_Ratio, 1};
                float radius = length(offset / ration);
                float deformation = 1 / pow(radius * pow(_Distance, 0.5), 2) * _Rad * 0.1;
                
                offset = offset * (1 - deformation);
                offset += _Position;
                
                half4 res = tex2D(_MainTex, offset);
                
                if(radius * _Distance < _Rad) 
                { 
                    res = half4( 0, 0, 0, 1 );
                }
                
                return res;
            }
            ENDCG
        }
    }
}
