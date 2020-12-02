// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "OceanShader"
{
	Properties
	{
		_WaveStretch("Wave Stretch", Vector) = (1,1,0,0)
		_WaveTile("Wave Tile", Float) = 1
		_WaveUp("Wave Up", Vector) = (0,1,0,0)
		_WaveHeight("Wave Height", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "bump" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float3 worldPos;
			float2 uv_texcoord;
		};

		uniform float3 _WaveUp;
		uniform float _WaveHeight;
		uniform float2 _WaveStretch;
		uniform float _WaveTile;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			float4 temp_cast_5 = (1.0).xxxx;
			return temp_cast_5;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float temp_output_9_0 = ( _Time.y * 0.2 );
			float2 temp_cast_0 = (1.0).xx;
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float4 appendResult12 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 worldSpaceTile13 = appendResult12;
			float4 WaveTileUV27 = ( ( worldSpaceTile13 * float4( _WaveStretch, 0.0 , 0.0 ) ) * _WaveTile );
			float2 panner5 = ( temp_output_9_0 * temp_cast_0 + WaveTileUV27.xy);
			float simplePerlin2D2 = snoise( panner5 );
			simplePerlin2D2 = simplePerlin2D2*0.5 + 0.5;
			float2 temp_cast_3 = (1.0).xx;
			float2 panner31 = ( temp_output_9_0 * temp_cast_3 + ( WaveTileUV27 * float4( 0.1,0.1,0,0 ) ).xy);
			float simplePerlin2D30 = snoise( panner31 );
			simplePerlin2D30 = simplePerlin2D30*0.5 + 0.5;
			float temp_output_33_0 = ( simplePerlin2D2 + simplePerlin2D30 );
			float3 WaveHeight39 = ( ( _WaveUp * _WaveHeight ) * temp_output_33_0 );
			v.vertex.xyz += WaveHeight39;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float3 tex2DNode44 = UnpackNormal( tex2D( _TextureSample0, uv_TextureSample0 ) );
			o.Normal = tex2DNode44;
			float temp_output_9_0 = ( _Time.y * 0.2 );
			float2 temp_cast_0 = (1.0).xx;
			float3 ase_worldPos = i.worldPos;
			float4 appendResult12 = (float4(ase_worldPos.x , ase_worldPos.z , 0.0 , 0.0));
			float4 worldSpaceTile13 = appendResult12;
			float4 WaveTileUV27 = ( ( worldSpaceTile13 * float4( _WaveStretch, 0.0 , 0.0 ) ) * _WaveTile );
			float2 panner5 = ( temp_output_9_0 * temp_cast_0 + WaveTileUV27.xy);
			float simplePerlin2D2 = snoise( panner5 );
			simplePerlin2D2 = simplePerlin2D2*0.5 + 0.5;
			float2 temp_cast_3 = (1.0).xx;
			float2 panner31 = ( temp_output_9_0 * temp_cast_3 + ( WaveTileUV27 * float4( 0.1,0.1,0,0 ) ).xy);
			float simplePerlin2D30 = snoise( panner31 );
			simplePerlin2D30 = simplePerlin2D30*0.5 + 0.5;
			float temp_output_33_0 = ( simplePerlin2D2 + simplePerlin2D30 );
			float WavePattern36 = temp_output_33_0;
			float4 color49 = IsGammaSpace() ? float4(0,0,0,0.6078432) : float4(0,0,0,0.6078432);
			float blendOpSrc51 = ( WavePattern36 * color49.a );
			float blendOpDest51 = 0.0;
			float lerpBlendMode51 = lerp(blendOpDest51,( 1.0 - ( ( 1.0 - blendOpDest51) / max( blendOpSrc51, 0.00001) ) ),color49.a);
			float3 temp_cast_5 = (( ( saturate( lerpBlendMode51 )) * simplePerlin2D30 )).xxx;
			o.Emission = temp_cast_5;
			o.Smoothness = 0.12;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18703
0;23;1596;669;976.3876;205.3042;1.749889;True;True
Node;AmplifyShaderEditor.CommentaryNode;14;-2920.436,-1014.288;Inherit;False;772.3921;275;Comment;3;11;12;13;World Space UVs;1,1,1,1;0;0
Node;AmplifyShaderEditor.WorldPosInputsNode;11;-2870.436,-964.2878;Inherit;True;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;12;-2552.12,-941.0995;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;13;-2375.044,-947.4236;Inherit;False;worldSpaceTile;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;41;-1756.77,-1163.226;Inherit;False;1323.232;1053.664;Comment;11;37;24;25;38;39;15;17;16;21;19;27;Wave UV's and Height;1,1,1,1;0;0
Node;AmplifyShaderEditor.Vector2Node;17;-1507.869,-878.957;Float;False;Property;_WaveStretch;Wave Stretch;0;0;Create;True;0;0;False;0;False;1,1;1.68,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.GetLocalVarNode;15;-1569.027,-1113.226;Inherit;True;13;worldSpaceTile;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1255.198,-1026.841;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1183.152,-800.7117;Float;True;Property;_WaveTile;Wave Tile;1;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;19;-934.3519,-1026.841;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;42;-2457.266,189.0759;Inherit;False;1961.372;784.9639;Comment;13;34;10;8;22;29;9;32;31;5;30;33;36;2;Wave Pattern;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;-657.5396,-903.2443;Inherit;True;WaveTileUV;-1;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;34;-2283.342,759.496;Inherit;False;27;WaveTileUV;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleTimeNode;8;-2284.999,542.7227;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-2279.778,647.0126;Float;False;Constant;_WaveSpeed;Wave Speed;1;0;Create;True;0;0;False;0;False;0.2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-2045.088,767.149;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0.1,0.1,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.GetLocalVarNode;29;-2265.898,239.0759;Inherit;False;27;WaveTileUV;1;0;OBJECT;;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-2079.733,582.4746;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-2279.49,412.6352;Float;False;Constant;_WaveDirection;Wave Direction;4;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;31;-1766.954,609.3734;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;5;-1761.628,392.178;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;2;-1401.894,335.8188;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;30;-1395.693,666.8024;Inherit;True;Simplex2D;True;False;2;0;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;33;-1061.608,366.0302;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-1497.132,-283.2404;Float;False;Property;_WaveHeight;Wave Height;3;0;Create;True;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;24;-1481.479,-515.9638;Float;False;Property;_WaveUp;Wave Up;2;0;Create;True;0;0;False;0;False;0,1,0;0,1,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RegisterLocalVarNode;36;-724.8803,603.1052;Inherit;False;WavePattern;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1291.772,-353.454;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;49;-389.8969,343.606;Inherit;False;Constant;_Color0;Color 0;5;0;Create;True;0;0;False;0;False;0,0,0,0.6078432;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;46;-368.9011,3.215801;Inherit;True;36;WavePattern;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-896.269,-352.5446;Inherit;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;50;-142.5273,256.866;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;39;-636.8176,-355.447;Inherit;True;WaveHeight;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.BlendOpsNode;51;34.16505,362.8813;Inherit;False;ColorBurn;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;44;225.8986,-634.9804;Inherit;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;False;-1;None;c0e93a4be86090f4eab7e514c9faa3a3;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;52;224.244,-169.087;Float;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;False;0;False;0.9;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;609.1478,624.5703;Float;False;Constant;_Tessellation;Tessellation;2;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;433.9409,-26.89812;Float;True;Constant;_Smoothness;Smoothness;3;0;Create;True;0;0;False;0;False;0.12;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;59;257.2837,456.1539;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;424.2893,-233.6249;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;40;581.3425,351.2498;Inherit;True;39;WaveHeight;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleTimeNode;53;219.0231,-271.9309;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;54;687.4479,-490.4354;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;55;224.5321,-403.4645;Float;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;859.1899,80.26341;Float;False;True;-1;6;ASEMaterialInspector;0;0;Standard;OceanShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;12;0;11;1
WireConnection;12;1;11;3
WireConnection;13;0;12;0
WireConnection;16;0;15;0
WireConnection;16;1;17;0
WireConnection;19;0;16;0
WireConnection;19;1;21;0
WireConnection;27;0;19;0
WireConnection;32;0;34;0
WireConnection;9;0;8;0
WireConnection;9;1;10;0
WireConnection;31;0;32;0
WireConnection;31;2;22;0
WireConnection;31;1;9;0
WireConnection;5;0;29;0
WireConnection;5;2;22;0
WireConnection;5;1;9;0
WireConnection;2;0;5;0
WireConnection;30;0;31;0
WireConnection;33;0;2;0
WireConnection;33;1;30;0
WireConnection;36;0;33;0
WireConnection;25;0;24;0
WireConnection;25;1;37;0
WireConnection;38;0;25;0
WireConnection;38;1;33;0
WireConnection;50;0;46;0
WireConnection;50;1;49;4
WireConnection;39;0;38;0
WireConnection;51;0;50;0
WireConnection;51;2;49;4
WireConnection;59;0;51;0
WireConnection;59;1;30;0
WireConnection;56;0;53;0
WireConnection;56;1;52;0
WireConnection;54;0;44;0
WireConnection;54;2;55;0
WireConnection;54;1;56;0
WireConnection;0;1;44;0
WireConnection;0;2;59;0
WireConnection;0;4;26;0
WireConnection;0;11;40;0
WireConnection;0;14;23;0
ASEEND*/
//CHKSM=673A048C27F352A592BF2482C20AC20E7BF5350E