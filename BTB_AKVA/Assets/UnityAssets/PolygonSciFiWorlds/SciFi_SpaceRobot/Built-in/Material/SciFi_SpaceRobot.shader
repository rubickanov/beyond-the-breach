// Made with Amplify Shader Editor v1.9.1.8
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "SciFi_SpaceRobot"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_Albedo_Color("Albedo_Color", Color) = (1,1,1,0)
		_Specular("Specular", 2D) = "white" {}
		_Smoothness("Smoothness", Range( 0 , 1)) = 0.55
		_AOcclusion("AOcclusion", 2D) = "white" {}
		[Normal]_Normal("Normal", 2D) = "bump" {}
		_NormalScale("Normal Scale", Range( 0 , 1)) = 1
		_Emission("Emission", 2D) = "black" {}
		[HDR]_EmissionColor("EmissionColor", Color) = (0.02842604,0.48515,0.9386859,1)
		[HDR]_GlowEmission("GlowEmission", Color) = (0.1651322,0.730461,0.9734455,1)
		_GN_GlowingSpeed("GN_GlowingSpeed", Float) = 2
		[HDR]_FlowEmission("FlowEmission", Color) = (0.02842604,0.48515,0.9386859,1)
		[KeywordEnum(Verticle,Horizontal)] _Flow_Direction("Flow_Direction", Float) = 0
		_Red_FlowSpeed("Red_FlowSpeed", Float) = 1
		_Emission_ColorMask("Emission_ColorMask", 2D) = "black" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityStandardUtils.cginc"
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _FLOW_DIRECTION_VERTICLE _FLOW_DIRECTION_HORIZONTAL
		#pragma surface surf StandardSpecular keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normal;
		uniform float _NormalScale;
		uniform float4 _Albedo_Color;
		uniform sampler2D _Albedo;
		uniform sampler2D _Emission_ColorMask;
		uniform float4 _Emission_ColorMask_ST;
		uniform float _GN_GlowingSpeed;
		uniform float4 _GlowEmission;
		uniform sampler2D _Emission;
		uniform float4 _Emission_ST;
		uniform float4 _EmissionColor;
		uniform float _Red_FlowSpeed;
		uniform float4 _FlowEmission;
		uniform sampler2D _Specular;
		uniform float _Smoothness;
		uniform sampler2D _AOcclusion;

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			o.Normal = UnpackScaleNormal( tex2D( _Normal, i.uv_texcoord ), _NormalScale );
			o.Albedo = ( _Albedo_Color * tex2D( _Albedo, i.uv_texcoord ) ).rgb;
			float2 uv_Emission_ColorMask = i.uv_texcoord * _Emission_ColorMask_ST.xy + _Emission_ColorMask_ST.zw;
			float4 tex2DNode10 = tex2D( _Emission_ColorMask, uv_Emission_ColorMask );
			float mulTime66 = _Time.y * _GN_GlowingSpeed;
			float2 uv_Emission = i.uv_texcoord * _Emission_ST.xy + _Emission_ST.zw;
			float4 tex2DNode3 = tex2D( _Emission, uv_Emission );
			float2 uv_TexCoord79 = i.uv_texcoord * float2( 15,15 );
			#if defined(_FLOW_DIRECTION_VERTICLE)
				float staticSwitch110 = uv_TexCoord79.y;
			#elif defined(_FLOW_DIRECTION_HORIZONTAL)
				float staticSwitch110 = uv_TexCoord79.x;
			#else
				float staticSwitch110 = uv_TexCoord79.y;
			#endif
			float mulTime80 = _Time.y * _Red_FlowSpeed;
			o.Emission = ( ( tex2DNode10.g * (0.5 + (sin( mulTime66 ) - -1.0) * (0.8 - 0.5) / (1.0 - -1.0)) * _GlowEmission * tex2DNode3 ) + ( ( _EmissionColor * tex2DNode3 ) * tex2DNode10.b ) + ( tex2DNode3 * tex2DNode10.r * (0.4 + (sin( ( ( 2.5 * staticSwitch110 ) + mulTime80 ) ) - -1.0) * (2.0 - 0.4) / (1.0 - -1.0)) * _FlowEmission ) ).rgb;
			o.Specular = tex2D( _Specular, i.uv_texcoord ).rgb;
			o.Smoothness = _Smoothness;
			o.Occlusion = tex2D( _AOcclusion, i.uv_texcoord ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19108
Node;AmplifyShaderEditor.TextureCoordinatesNode;79;-2403.901,470.8728;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;15,15;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;82;-2027.264,395.742;Inherit;False;Constant;_Red_FlowWidth;Red_FlowWidth;16;0;Create;True;0;0;0;False;0;False;2.5;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;110;-2116.363,490.9539;Inherit;False;Property;_Flow_Direction;Flow_Direction;12;0;Create;True;0;0;0;False;0;False;0;0;0;True;;KeywordEnum;2;Verticle;Horizontal;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;81;-2225.076,700.2019;Inherit;False;Property;_Red_FlowSpeed;Red_FlowSpeed;13;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;77;-1842.802,470.1613;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;65;-2239.917,97.61084;Float;False;Property;_GN_GlowingSpeed;GN_GlowingSpeed;10;0;Create;True;0;0;0;False;0;False;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;80;-2030.705,674.4002;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;83;-1694.178,549.8092;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;66;-2009.793,98.80493;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;67;-1799.763,107.6117;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;84;-1528.365,533.2954;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;3;-1881.893,-401.3672;Inherit;True;Property;_Emission;Emission;7;0;Create;True;0;0;0;False;0;False;-1;6cf6990432de0b34f806a6e65a828c12;6cf6990432de0b34f806a6e65a828c12;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;18;-1807.844,-624.4033;Float;False;Property;_EmissionColor;EmissionColor;8;1;[HDR];Create;True;0;0;0;False;0;False;0.02842604,0.48515,0.9386859,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-1804.594,-141.6173;Inherit;True;Property;_Emission_ColorMask;Emission_ColorMask;14;0;Create;True;0;0;0;False;0;False;-1;f59a2024b3e9c1140a16f076e44c2cfc;f59a2024b3e9c1140a16f076e44c2cfc;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;103;-1174.158,-682.2048;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;68;-1661.612,92.79103;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0.5;False;4;FLOAT;0.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;7;-1540.822,292.0281;Float;False;Property;_GlowEmission;GlowEmission;9;1;[HDR];Create;True;0;0;0;False;0;False;0.1651322,0.730461,0.9734455,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;91;-1269.811,779.4674;Float;False;Property;_FlowEmission;FlowEmission;11;1;[HDR];Create;True;0;0;0;False;0;False;0.02842604,0.48515,0.9386859,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;88;-1210.805,554.7167;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0.4;False;4;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-1294.086,-142.493;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;107;-899.6614,533.2916;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-690.3475,-632.5135;Inherit;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;0;False;0;False;-1;0573beb72e12c134aa552003d7d69705;0573beb72e12c134aa552003d7d69705;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;6;-906.5624,-346.3206;Float;False;Property;_NormalScale;Normal Scale;6;0;Create;True;0;0;0;False;0;False;1;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;101;-358.3875,-708.0792;Inherit;False;Property;_Albedo_Color;Albedo_Color;1;0;Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-1005.383,18.61914;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;90;-1015.698,-91.40713;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;89;-719.4376,-95.72168;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;4;-619.6537,534.908;Inherit;True;Property;_AOcclusion;AOcclusion;4;0;Create;True;0;0;0;False;0;False;-1;42344926bb1b3114b8bd667914cd19f5;42344926bb1b3114b8bd667914cd19f5;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;54;-611.5583,416.399;Float;False;Property;_Smoothness;Smoothness;3;0;Create;True;0;0;0;False;0;False;0.55;0.7;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-614.3014,-359.3794;Inherit;True;Property;_Normal;Normal;5;1;[Normal];Create;True;0;0;0;False;0;False;-1;e25b09a6d28ae1a47905e79e3179dccc;e25b09a6d28ae1a47905e79e3179dccc;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;102;-227.136,-510.3268;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;5;-638.7391,112.4848;Inherit;True;Property;_Specular;Specular;2;0;Create;True;0;0;0;False;0;False;-1;aa903b8b6452cd9469114bf865d681b4;aa903b8b6452cd9469114bf865d681b4;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;StandardSpecular;SciFi_SpaceRobot;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;0;False;;False;0;False;;0;False;;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;2;15;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;0;False;;0;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;110;1;79;2
WireConnection;110;0;79;1
WireConnection;77;0;82;0
WireConnection;77;1;110;0
WireConnection;80;0;81;0
WireConnection;83;0;77;0
WireConnection;83;1;80;0
WireConnection;66;0;65;0
WireConnection;67;0;66;0
WireConnection;84;0;83;0
WireConnection;68;0;67;0
WireConnection;88;0;84;0
WireConnection;9;0;18;0
WireConnection;9;1;3;0
WireConnection;107;0;3;0
WireConnection;107;1;10;1
WireConnection;107;2;88;0
WireConnection;107;3;91;0
WireConnection;1;1;103;0
WireConnection;75;0;10;2
WireConnection;75;1;68;0
WireConnection;75;2;7;0
WireConnection;75;3;3;0
WireConnection;90;0;9;0
WireConnection;90;1;10;3
WireConnection;89;0;75;0
WireConnection;89;1;90;0
WireConnection;89;2;107;0
WireConnection;4;1;103;0
WireConnection;2;1;103;0
WireConnection;2;5;6;0
WireConnection;102;0;101;0
WireConnection;102;1;1;0
WireConnection;5;1;103;0
WireConnection;0;0;102;0
WireConnection;0;1;2;0
WireConnection;0;2;89;0
WireConnection;0;3;5;0
WireConnection;0;4;54;0
WireConnection;0;5;4;0
ASEEND*/
//CHKSM=1E8D6D73266FF2BFBB687515D38B8D024D00BB32