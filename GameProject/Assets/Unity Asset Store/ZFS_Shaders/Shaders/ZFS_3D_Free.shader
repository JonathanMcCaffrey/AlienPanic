// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:2,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:False,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-2476-OUT;n:type:ShaderForge.SFN_Multiply,id:890,x:33420,y:32726|A-1876-RGB,B-1665-OUT;n:type:ShaderForge.SFN_Add,id:892,x:35639,y:33293|A-1805-OUT,B-1771-OUT;n:type:ShaderForge.SFN_Fresnel,id:893,x:36105,y:33164|EXP-1173-OUT;n:type:ShaderForge.SFN_Multiply,id:895,x:34733,y:32816|A-1316-R,B-892-OUT;n:type:ShaderForge.SFN_Tex2d,id:896,x:35900,y:32379,ptlb:Texture,ptin:_Texture,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-923-UVOUT;n:type:ShaderForge.SFN_Panner,id:923,x:36228,y:32381,spu:0,spv:0.1|DIST-2436-OUT;n:type:ShaderForge.SFN_Add,id:974,x:34325,y:32793|A-1898-OUT,B-1771-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1173,x:36265,y:33182,ptlb:Fresnel_Exponent,ptin:_Fresnel_Exponent,glob:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:1316,x:34994,y:32767,ptlb:Gradient_Texture_Decay,ptin:_Gradient_Texture_Decay,ntxv:0,isnm:False|UVIN-1319-OUT;n:type:ShaderForge.SFN_Append,id:1319,x:35171,y:32767|A-1948-OUT,B-1359-OUT;n:type:ShaderForge.SFN_TexCoord,id:1336,x:36217,y:32669,uv:0;n:type:ShaderForge.SFN_Multiply,id:1340,x:36039,y:32755|A-1336-V,B-1342-OUT;n:type:ShaderForge.SFN_Vector1,id:1342,x:36217,y:32817,v1:0;n:type:ShaderForge.SFN_Add,id:1359,x:35865,y:32857|A-1340-OUT,B-2056-OUT;n:type:ShaderForge.SFN_Add,id:1497,x:35614,y:32767|A-896-R,B-1805-OUT;n:type:ShaderForge.SFN_TexCoord,id:1618,x:36280,y:33504,uv:0;n:type:ShaderForge.SFN_Tex2d,id:1620,x:36120,y:33504,ptlb:Gradient_Edge_Fake,ptin:_Gradient_Edge_Fake,tex:dd02884263250074096b0d1643f90f41,ntxv:0,isnm:False|UVIN-1618-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1652,x:33776,y:32948,uv:0;n:type:ShaderForge.SFN_Append,id:1654,x:33595,y:32888|A-1665-OUT,B-1652-V;n:type:ShaderForge.SFN_Tex2d,id:1656,x:33414,y:32888,ptlb:Gradient_Color,ptin:_Gradient_Color,ntxv:0,isnm:False|UVIN-1654-OUT;n:type:ShaderForge.SFN_Clamp,id:1665,x:33790,y:32784|IN-2450-OUT,MIN-1667-OUT,MAX-1666-OUT;n:type:ShaderForge.SFN_Vector1,id:1666,x:33986,y:32754,v1:0.95;n:type:ShaderForge.SFN_Vector1,id:1667,x:33986,y:32700,v1:0.05;n:type:ShaderForge.SFN_SwitchProperty,id:1771,x:35957,y:33504,ptlb:Edge_Detection_Fake,ptin:_Edge_Detection_Fake,on:True|A-1772-OUT,B-1620-R;n:type:ShaderForge.SFN_Vector1,id:1772,x:36161,y:33384,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:1805,x:35948,y:33146,ptlb:Fresnel,ptin:_Fresnel,on:True|A-1772-OUT,B-893-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1858,x:33234,y:32811,ptlb:Gradient_Or_Solid_Color,ptin:_Gradient_Or_Solid_Color,on:True|A-890-OUT,B-1656-RGB;n:type:ShaderForge.SFN_Color,id:1876,x:33627,y:32660,ptlb:Solid_Color,ptin:_Solid_Color,glob:False,c1:0.1764706,c2:0.5229208,c3:1,c4:1;n:type:ShaderForge.SFN_SwitchProperty,id:1898,x:34509,y:32793,ptlb:Make_Same_As_Fresnel,ptin:_Make_Same_As_Fresnel,on:True|A-1316-R,B-895-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1948,x:35378,y:32767,ptlb:Soft_Texture,ptin:_Soft_Texture,on:False|A-1497-OUT,B-896-R;n:type:ShaderForge.SFN_Slider,id:2056,x:36039,y:32939,ptlb:Decay,ptin:_Decay,min:0.05,cur:0.3,max:0.95;n:type:ShaderForge.SFN_Time,id:2434,x:36557,y:32378;n:type:ShaderForge.SFN_Multiply,id:2436,x:36386,y:32402|A-2434-T,B-2438-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2438,x:36557,y:32526,ptlb:Pan_Speed,ptin:_Pan_Speed,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2450,x:34098,y:32835|A-974-OUT,B-2452-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2452,x:34325,y:32952,ptlb:Intensity,ptin:_Intensity,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2476,x:33048,y:32811|A-1858-OUT,B-2477-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2477,x:33234,y:32955,ptlb:Brightness,ptin:_Brightness,glob:False,v1:1;proporder:2477-2452-2438-1858-1656-1876-896-1316-2056-1805-1898-1173-1771-1620-1948;pass:END;sub:END;*/

Shader "ZFS Shaders/ZFS_3D_Free" {
    Properties {
        _Brightness ("Brightness", Float ) = 1
        _Intensity ("Intensity", Float ) = 1
        _Pan_Speed ("Pan_Speed", Float ) = 1
        [MaterialToggle] _Gradient_Or_Solid_Color ("Gradient_Or_Solid_Color", Float ) = 1
        _Gradient_Color ("Gradient_Color", 2D) = "white" {}
        _Solid_Color ("Solid_Color", Color) = (0.1764706,0.5229208,1,1)
        _Texture ("Texture", 2D) = "white" {}
        _Gradient_Texture_Decay ("Gradient_Texture_Decay", 2D) = "white" {}
        _Decay ("Decay", Range(0.05, 0.95)) = 0.3
        [MaterialToggle] _Fresnel ("Fresnel", Float ) = 0
        [MaterialToggle] _Make_Same_As_Fresnel ("Make_Same_As_Fresnel", Float ) = 0.2901961
        _Fresnel_Exponent ("Fresnel_Exponent", Float ) = 1
        [MaterialToggle] _Edge_Detection_Fake ("Edge_Detection_Fake", Float ) = 0.2901961
        _Gradient_Edge_Fake ("Gradient_Edge_Fake", 2D) = "white" {}
        [MaterialToggle] _Soft_Texture ("Soft_Texture", Float ) = 0.6980392
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "ForwardBase"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers xbox360 ps3 flash d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Fresnel_Exponent;
            uniform sampler2D _Gradient_Texture_Decay; uniform float4 _Gradient_Texture_Decay_ST;
            uniform sampler2D _Gradient_Edge_Fake; uniform float4 _Gradient_Edge_Fake_ST;
            uniform sampler2D _Gradient_Color; uniform float4 _Gradient_Color_ST;
            uniform fixed _Edge_Detection_Fake;
            uniform fixed _Fresnel;
            uniform fixed _Gradient_Or_Solid_Color;
            uniform float4 _Solid_Color;
            uniform fixed _Make_Same_As_Fresnel;
            uniform fixed _Soft_Texture;
            uniform float _Decay;
            uniform float _Pan_Speed;
            uniform float _Intensity;
            uniform float _Brightness;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.normalDir = mul(float4(v.normal,0), _World2Object).xyz;
                o.posWorld = mul(_Object2World, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
/////// Normals:
                float3 normalDirection =  i.normalDir;
                
                float nSign = sign( dot( viewDirection, i.normalDir ) ); // Reverse normal if this is a backface
                i.normalDir *= nSign;
                normalDirection *= nSign;
                
////// Lighting:
////// Emissive:
                float4 node_2434 = _Time + _TimeEditor;
                float2 node_923 = (i.uv0.rg+(node_2434.g*_Pan_Speed)*float2(0,0.1));
                float4 node_896 = tex2D(_Texture,TRANSFORM_TEX(node_923, _Texture));
                float node_1772 = 0.0;
                float node_1805 = lerp( node_1772, pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel_Exponent), _Fresnel );
                float2 node_1319 = float2(lerp( (node_896.r+node_1805), node_896.r, _Soft_Texture ),((i.uv0.g*0.0)+_Decay));
                float4 node_1316 = tex2D(_Gradient_Texture_Decay,TRANSFORM_TEX(node_1319, _Gradient_Texture_Decay));
                float2 node_1618 = i.uv0;
                float node_1771 = lerp( node_1772, tex2D(_Gradient_Edge_Fake,TRANSFORM_TEX(node_1618.rg, _Gradient_Edge_Fake)).r, _Edge_Detection_Fake );
                float node_1665 = clamp(((lerp( node_1316.r, (node_1316.r*(node_1805+node_1771)), _Make_Same_As_Fresnel )+node_1771)*_Intensity),0.05,0.95);
                float2 node_1654 = float2(node_1665,i.uv0.g);
                float3 emissive = (lerp( (_Solid_Color.rgb*node_1665), tex2D(_Gradient_Color,TRANSFORM_TEX(node_1654, _Gradient_Color)).rgb, _Gradient_Or_Solid_Color )*_Brightness);
                float3 finalColor = emissive;
/// Final Color:
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
