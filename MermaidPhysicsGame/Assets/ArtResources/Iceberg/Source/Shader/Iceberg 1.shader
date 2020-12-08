// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:Standard (Specular setup),iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33927,y:32777,varname:node_2865,prsc:2|diff-2618-OUT,spec-290-OUT,gloss-2405-OUT,normal-2972-OUT,emission-1841-OUT,transm-4556-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:31405,y:32283,varname:node_6343,prsc:2|A-7736-RGB,B-6665-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:31082,y:32513,ptovrint:False,ptlb:Base Color,ptin:_BaseColor,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:31068,y:32208,ptovrint:True,ptlb:Base Texture,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:31682,y:33175,ptovrint:True,ptlb:Base Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:358,x:31697,y:32964,ptovrint:False,ptlb:Specular intensity,ptin:_Specularintensity,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:1813,x:31988,y:33063,ptovrint:False,ptlb:Gloss inttensity,ptin:_Glossinttensity,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Tex2d,id:2601,x:33691,y:32326,ptovrint:False,ptlb:Emissive Texture,ptin:_EmissiveTexture,varname:node_2601,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:7873,x:31688,y:32317,varname:node_7873,prsc:2|A-6343-OUT,B-4951-OUT;n:type:ShaderForge.SFN_Power,id:4951,x:31431,y:32608,varname:node_4951,prsc:2|VAL-7736-A,EXP-9830-OUT;n:type:ShaderForge.SFN_Slider,id:9830,x:31030,y:32928,ptovrint:False,ptlb:AO Intensity,ptin:_AOIntensity,varname:node_9830,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Multiply,id:290,x:32274,y:32672,varname:node_290,prsc:2|A-3349-RGB,B-358-OUT;n:type:ShaderForge.SFN_Tex2d,id:3349,x:31811,y:32582,ptovrint:False,ptlb:Base Specular,ptin:_BaseSpecular,varname:node_3349,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2405,x:32574,y:32871,varname:node_2405,prsc:2|A-3349-A,B-1813-OUT;n:type:ShaderForge.SFN_Vector3,id:9300,x:31735,y:33734,varname:node_9300,prsc:2,v1:0.5,v2:0.5,v3:0;n:type:ShaderForge.SFN_Multiply,id:6535,x:32033,y:33600,varname:node_6535,prsc:2|A-5039-RGB,B-9300-OUT,C-9716-OUT;n:type:ShaderForge.SFN_Add,id:2972,x:32336,y:33383,varname:node_2972,prsc:2|A-6594-OUT,B-6535-OUT;n:type:ShaderForge.SFN_Tex2d,id:5039,x:31777,y:33510,ptovrint:False,ptlb:Detail Normal Map,ptin:_DetailNormalMap,varname:node_5039,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Slider,id:9716,x:31735,y:33861,ptovrint:False,ptlb:Detail Normal Intensity,ptin:_DetailNormalIntensity,varname:node_9716,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Vector3,id:9535,x:31762,y:33376,varname:node_9535,prsc:2,v1:0.5,v2:0.5,v3:1;n:type:ShaderForge.SFN_Slider,id:9054,x:31916,y:33454,ptovrint:False,ptlb:Base Normal Intensity,ptin:_BaseNormalIntensity,varname:node_9054,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:6594,x:32073,y:33206,varname:node_6594,prsc:2|A-9535-OUT,B-5964-RGB,T-9054-OUT;n:type:ShaderForge.SFN_NormalVector,id:9089,x:30236,y:33966,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:7867,x:29629,y:35055,varname:node_7867,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4556,x:33237,y:34047,varname:node_4556,prsc:2|A-906-OUT,B-7473-RGB;n:type:ShaderForge.SFN_LightColor,id:7473,x:32767,y:34471,varname:node_7473,prsc:2;n:type:ShaderForge.SFN_Add,id:5804,x:29992,y:34886,varname:node_5804,prsc:2|A-9089-OUT,B-7867-OUT;n:type:ShaderForge.SFN_Negate,id:6908,x:30504,y:34871,varname:node_6908,prsc:2|IN-7837-OUT;n:type:ShaderForge.SFN_Dot,id:3001,x:30923,y:34938,varname:node_3001,prsc:2,dt:1|A-6908-OUT,B-5863-OUT;n:type:ShaderForge.SFN_Multiply,id:7837,x:30315,y:34980,varname:node_7837,prsc:2|A-5804-OUT,B-4039-OUT;n:type:ShaderForge.SFN_Slider,id:4039,x:29972,y:35035,ptovrint:False,ptlb:Translucency Intensity,ptin:_TranslucencyIntensity,varname:node_1602,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:5,max:5;n:type:ShaderForge.SFN_Power,id:3300,x:31416,y:34981,varname:node_3300,prsc:2|VAL-3001-OUT,EXP-8119-OUT;n:type:ShaderForge.SFN_Multiply,id:906,x:31842,y:34665,varname:node_906,prsc:2|A-3300-OUT,B-3125-RGB;n:type:ShaderForge.SFN_ViewVector,id:5863,x:30582,y:35170,varname:node_5863,prsc:2;n:type:ShaderForge.SFN_Slider,id:8119,x:31082,y:35270,ptovrint:False,ptlb:Translucency Width,ptin:_TranslucencyWidth,varname:node_2119,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:4.31186,max:8;n:type:ShaderForge.SFN_Color,id:3125,x:31644,y:35186,ptovrint:False,ptlb:Translucency Color,ptin:_TranslucencyColor,varname:node_8966,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Tex2d,id:525,x:31731,y:31941,ptovrint:False,ptlb:Detail Texture,ptin:_DetailTexture,varname:node_525,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:5010,x:31872,y:32401,ptovrint:False,ptlb:Detail Texture intensity,ptin:_DetailTextureintensity,varname:node_5010,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:23,x:32143,y:32032,varname:node_23,prsc:2|A-7873-OUT,B-525-RGB,T-5010-OUT;n:type:ShaderForge.SFN_Color,id:2471,x:32754,y:33475,ptovrint:False,ptlb:Fresnel Color,ptin:_FresnelColor,varname:node_2471,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4705882,c2:0.4705882,c3:0.4705882,c4:1;n:type:ShaderForge.SFN_Fresnel,id:2324,x:32799,y:33651,varname:node_2324,prsc:2|EXP-5494-OUT;n:type:ShaderForge.SFN_Multiply,id:1888,x:33162,y:33265,varname:node_1888,prsc:2|A-2471-RGB,B-2324-OUT,C-1308-OUT;n:type:ShaderForge.SFN_Slider,id:1308,x:32975,y:33703,ptovrint:False,ptlb:Fresnel intensity,ptin:_Fresnelintensity,varname:node_1308,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Slider,id:5494,x:32442,y:33638,ptovrint:False,ptlb:Fresnel Radius,ptin:_FresnelRadius,varname:node_5494,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:1,max:0;n:type:ShaderForge.SFN_Add,id:2618,x:33227,y:32292,varname:node_2618,prsc:2|A-23-OUT,B-1888-OUT;n:type:ShaderForge.SFN_Color,id:6118,x:32166,y:34496,ptovrint:False,ptlb:Trans_Color_copy_copy_copy,ptin:_Trans_Color_copy_copy_copy,varname:_Trans_Color_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:8376,x:32230,y:34560,ptovrint:False,ptlb:Trans_Color_copy_copy_copy_copy,ptin:_Trans_Color_copy_copy_copy_copy,varname:_Trans_Color_copy_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Color,id:608,x:32294,y:34624,ptovrint:False,ptlb:Trans_Color_copy_copy_copy_copy_copy,ptin:_Trans_Color_copy_copy_copy_copy_copy,varname:_Trans_Color_copy_copy_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:1841,x:33976,y:32413,varname:node_1841,prsc:2|A-2601-RGB,B-2312-OUT;n:type:ShaderForge.SFN_Slider,id:2312,x:33495,y:32577,ptovrint:False,ptlb:Emissive,ptin:_Emissive,varname:node_2312,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:6665-7736-5964-9054-3349-358-1813-2601-9830-5039-9716-525-5010-4039-8119-3125-2471-1308-5494-2312;pass:END;sub:END;*/

Shader "DzenGames/Iceberg" {
    Properties {
        _BaseColor ("Base Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _MainTex ("Base Texture", 2D) = "white" {}
        _BumpMap ("Base Normal Map", 2D) = "bump" {}
        _BaseNormalIntensity ("Base Normal Intensity", Range(0, 1)) = 0
        _BaseSpecular ("Base Specular", 2D) = "white" {}
        _Specularintensity ("Specular intensity", Range(0, 1)) = 1
        _Glossinttensity ("Gloss inttensity", Range(0, 1)) = 1
        _EmissiveTexture ("Emissive Texture", 2D) = "white" {}
        _AOIntensity ("AO Intensity", Range(0, 10)) = 0
        _DetailNormalMap ("Detail Normal Map", 2D) = "bump" {}
        _DetailNormalIntensity ("Detail Normal Intensity", Range(0, 2)) = 0
        _DetailTexture ("Detail Texture", 2D) = "white" {}
        _DetailTextureintensity ("Detail Texture intensity", Range(0, 1)) = 0
        _TranslucencyIntensity ("Translucency Intensity", Range(0.1, 5)) = 5
        _TranslucencyWidth ("Translucency Width", Range(0.1, 8)) = 4.31186
        _TranslucencyColor ("Translucency Color", Color) = (0.5,0.5,0.5,1)
        _FresnelColor ("Fresnel Color", Color) = (0.4705882,0.4705882,0.4705882,1)
        _Fresnelintensity ("Fresnel intensity", Range(0, 1)) = 1
        _FresnelRadius ("Fresnel Radius", Range(1, 0)) = 1
        _Emissive ("Emissive", Range(0, 1)) = 0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles metal d3d11_9x n3ds wiiu 
            #pragma target 3.0
            uniform float4 _BaseColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Specularintensity;
            uniform float _Glossinttensity;
            uniform sampler2D _EmissiveTexture; uniform float4 _EmissiveTexture_ST;
            uniform float _AOIntensity;
            uniform sampler2D _BaseSpecular; uniform float4 _BaseSpecular_ST;
            uniform sampler2D _DetailNormalMap; uniform float4 _DetailNormalMap_ST;
            uniform float _DetailNormalIntensity;
            uniform float _BaseNormalIntensity;
            uniform float _TranslucencyIntensity;
            uniform float _TranslucencyWidth;
            uniform float4 _TranslucencyColor;
            uniform sampler2D _DetailTexture; uniform float4 _DetailTexture_ST;
            uniform float _DetailTextureintensity;
            uniform float4 _FresnelColor;
            uniform float _Fresnelintensity;
            uniform float _FresnelRadius;
            uniform float _Emissive;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD10;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 _DetailNormalMap_var = UnpackNormal(tex2D(_DetailNormalMap,TRANSFORM_TEX(i.uv0, _DetailNormalMap)));
                float3 normalLocal = (lerp(float3(0.5,0.5,1),_BumpMap_var.rgb,_BaseNormalIntensity)+(_DetailNormalMap_var.rgb*float3(0.5,0.5,0)*_DetailNormalIntensity));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _BaseSpecular_var = tex2D(_BaseSpecular,TRANSFORM_TEX(i.uv0, _BaseSpecular));
                float gloss = (_BaseSpecular_var.a*_Glossinttensity);
                float perceptualRoughness = 1.0 - (_BaseSpecular_var.a*_Glossinttensity);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = (_BaseSpecular_var.rgb*_Specularintensity);
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _DetailTexture_var = tex2D(_DetailTexture,TRANSFORM_TEX(i.uv0, _DetailTexture));
                float3 diffuseColor = (lerp(((_MainTex_var.rgb*_BaseColor.rgb)*pow(_MainTex_var.a,_AOIntensity)),_DetailTexture_var.rgb,_DetailTextureintensity)+(_FresnelColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRadius)*_Fresnelintensity)); // Need this for specular when using metallic
                diffuseColor = EnergyConservationBetweenDiffuseAndSpecular(diffuseColor, specularColor, specularMonochrome);
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float3 forwardLight = max(0.0, NdotL );
                float3 backLight = max(0.0, -NdotL ) * ((pow(max(0,dot((-1*((i.normalDir+lightDirection)*_TranslucencyIntensity)),viewDirection)),_TranslucencyWidth)*_TranslucencyColor.rgb)*_LightColor0.rgb);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 NdotLWrap = max(0,NdotL);
                float nlPow5 = Pow5(1-NdotLWrap);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((forwardLight+backLight) + ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL)) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 _EmissiveTexture_var = tex2D(_EmissiveTexture,TRANSFORM_TEX(i.uv0, _EmissiveTexture));
                float3 emissive = (_EmissiveTexture_var.rgb*_Emissive);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles metal d3d11_9x n3ds wiiu 
            #pragma target 3.0
            uniform float4 _BaseColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Specularintensity;
            uniform float _Glossinttensity;
            uniform sampler2D _EmissiveTexture; uniform float4 _EmissiveTexture_ST;
            uniform float _AOIntensity;
            uniform sampler2D _BaseSpecular; uniform float4 _BaseSpecular_ST;
            uniform sampler2D _DetailNormalMap; uniform float4 _DetailNormalMap_ST;
            uniform float _DetailNormalIntensity;
            uniform float _BaseNormalIntensity;
            uniform float _TranslucencyIntensity;
            uniform float _TranslucencyWidth;
            uniform float4 _TranslucencyColor;
            uniform sampler2D _DetailTexture; uniform float4 _DetailTexture_ST;
            uniform float _DetailTextureintensity;
            uniform float4 _FresnelColor;
            uniform float _Fresnelintensity;
            uniform float _FresnelRadius;
            uniform float _Emissive;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 _DetailNormalMap_var = UnpackNormal(tex2D(_DetailNormalMap,TRANSFORM_TEX(i.uv0, _DetailNormalMap)));
                float3 normalLocal = (lerp(float3(0.5,0.5,1),_BumpMap_var.rgb,_BaseNormalIntensity)+(_DetailNormalMap_var.rgb*float3(0.5,0.5,0)*_DetailNormalIntensity));
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _BaseSpecular_var = tex2D(_BaseSpecular,TRANSFORM_TEX(i.uv0, _BaseSpecular));
                float gloss = (_BaseSpecular_var.a*_Glossinttensity);
                float perceptualRoughness = 1.0 - (_BaseSpecular_var.a*_Glossinttensity);
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = (_BaseSpecular_var.rgb*_Specularintensity);
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _DetailTexture_var = tex2D(_DetailTexture,TRANSFORM_TEX(i.uv0, _DetailTexture));
                float3 diffuseColor = (lerp(((_MainTex_var.rgb*_BaseColor.rgb)*pow(_MainTex_var.a,_AOIntensity)),_DetailTexture_var.rgb,_DetailTextureintensity)+(_FresnelColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRadius)*_Fresnelintensity)); // Need this for specular when using metallic
                diffuseColor = EnergyConservationBetweenDiffuseAndSpecular(diffuseColor, specularColor, specularMonochrome);
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float3 forwardLight = max(0.0, NdotL );
                float3 backLight = max(0.0, -NdotL ) * ((pow(max(0,dot((-1*((i.normalDir+lightDirection)*_TranslucencyIntensity)),viewDirection)),_TranslucencyWidth)*_TranslucencyColor.rgb)*_LightColor0.rgb);
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 NdotLWrap = max(0,NdotL);
                float nlPow5 = Pow5(1-NdotLWrap);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((forwardLight+backLight) + ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL)) * attenColor;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles metal d3d11_9x n3ds wiiu 
            #pragma target 3.0
            uniform float4 _BaseColor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Specularintensity;
            uniform float _Glossinttensity;
            uniform sampler2D _EmissiveTexture; uniform float4 _EmissiveTexture_ST;
            uniform float _AOIntensity;
            uniform sampler2D _BaseSpecular; uniform float4 _BaseSpecular_ST;
            uniform sampler2D _DetailTexture; uniform float4 _DetailTexture_ST;
            uniform float _DetailTextureintensity;
            uniform float4 _FresnelColor;
            uniform float _Fresnelintensity;
            uniform float _FresnelRadius;
            uniform float _Emissive;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _EmissiveTexture_var = tex2D(_EmissiveTexture,TRANSFORM_TEX(i.uv0, _EmissiveTexture));
                o.Emission = (_EmissiveTexture_var.rgb*_Emissive);
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float4 _DetailTexture_var = tex2D(_DetailTexture,TRANSFORM_TEX(i.uv0, _DetailTexture));
                float3 diffColor = (lerp(((_MainTex_var.rgb*_BaseColor.rgb)*pow(_MainTex_var.a,_AOIntensity)),_DetailTexture_var.rgb,_DetailTextureintensity)+(_FresnelColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelRadius)*_Fresnelintensity));
                float4 _BaseSpecular_var = tex2D(_BaseSpecular,TRANSFORM_TEX(i.uv0, _BaseSpecular));
                float3 specColor = (_BaseSpecular_var.rgb*_Specularintensity);
                float specularMonochrome = max(max(specColor.r, specColor.g),specColor.b);
                diffColor *= (1.0-specularMonochrome);
                float roughness = 1.0 - (_BaseSpecular_var.a*_Glossinttensity);
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
