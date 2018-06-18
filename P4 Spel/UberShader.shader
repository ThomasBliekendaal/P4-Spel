Shader "hidden/preview"
{
	Properties
	{
				Color_14E04212("ShaderColor", Color) = (0,0,0,0)
				[NoScaleOffset] Texture_D06602EE("DissolveTexture", 2D) = "white" {}
				[NoScaleOffset] Texture_19E1D19C("NormalMap", 2D) = "white" {}
				[NoScaleOffset] Texture_69D72917("MetallicMap", 2D) = "white" {}
	}
	CGINCLUDE
	#include "UnityCG.cginc"
			void Unity_NormalCreate_float(Texture2D Texture, SamplerState Sampler, float2 UV, float Offset, float Strength, out float3 Out)
			{
			    Offset = pow(Offset, 3) * 0.1;
			    float2 offsetU = float2(UV.x + Offset, UV.y);
			    float2 offsetV = float2(UV.x, UV.y + Offset);
			    float normalSample = Texture.Sample(Sampler, UV);
			    float uSample = Texture.Sample(Sampler, offsetU);
			    float vSample = Texture.Sample(Sampler, offsetV);
			    float3 va = float3(1, 0, (uSample - normalSample) * Strength);
			    float3 vb = float3(0, 1, (vSample - normalSample) * Strength);
			    Out = normalize(cross(va, vb));
			}
			struct GraphVertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float4 texcoord0 : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			struct SurfaceInputs{
				half4 uv0;
			};
			struct SurfaceDescription{
				float4 PreviewOutput;
			};
			float Float_196CD6A3;
			float4 Color_14E04212;
			UNITY_DECLARE_TEX2D(Texture_D06602EE);
			UNITY_DECLARE_TEX2D(Texture_19E1D19C);
			UNITY_DECLARE_TEX2D(Texture_69D72917);
			float4 _NormalCreate_9FCCDBFE_UV;
			float _NormalCreate_9FCCDBFE_Offset;
			float _NormalCreate_9FCCDBFE_Strength;
			GraphVertexInput PopulateVertexData(GraphVertexInput v){
				return v;
			}
			SurfaceDescription PopulateSurfaceData(SurfaceInputs IN) {
				SurfaceDescription surface = (SurfaceDescription)0;
				half4 uv0 = IN.uv0;
				float3 _NormalCreate_9FCCDBFE_Out;
				Unity_NormalCreate_float(Texture_19E1D19C, samplerTexture_19E1D19C, uv0.xy, _NormalCreate_9FCCDBFE_Offset, _NormalCreate_9FCCDBFE_Strength, _NormalCreate_9FCCDBFE_Out);
				if (Float_196CD6A3 == 0) { surface.PreviewOutput = half4(_NormalCreate_9FCCDBFE_Out.x, _NormalCreate_9FCCDBFE_Out.y, _NormalCreate_9FCCDBFE_Out.z, 1.0); return surface; }
				float4 _Property_82548747_Out = Color_14E04212;
				return surface;
			}
	ENDCG
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
	        struct GraphVertexOutput
	        {
	            float4 position : POSITION;
	            half4 uv0 : TEXCOORD;
	        };
	        GraphVertexOutput vert (GraphVertexInput v)
	        {
	            v = PopulateVertexData(v);
	            GraphVertexOutput o;
	            o.position = UnityObjectToClipPos(v.vertex);
	            o.uv0 = v.texcoord0;
	            return o;
	        }
	        fixed4 frag (GraphVertexOutput IN) : SV_Target
	        {
	            float4 uv0  = IN.uv0;
	            SurfaceInputs surfaceInput = (SurfaceInputs)0;;
	            surfaceInput.uv0 = uv0;
	            SurfaceDescription surf = PopulateSurfaceData(surfaceInput);
	            return surf.PreviewOutput;
	        }
	        ENDCG
	    }
	}
}
