// Shader created with Shader Forge Beta 0.34 
// Shader Forge (c) Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:0.34;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,limd:0,uamb:True,mssp:True,lmpd:False,lprd:False,enco:False,frtr:True,vitr:True,dbil:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,blpr:2,bsrc:0,bdst:0,culm:2,dpts:2,wrdp:False,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:1,x:32719,y:32712|emission-2392-OUT;n:type:ShaderForge.SFN_Multiply,id:890,x:33357,y:32726|A-1876-RGB,B-1665-OUT;n:type:ShaderForge.SFN_Add,id:892,x:35730,y:33256|A-1805-OUT,B-1771-OUT;n:type:ShaderForge.SFN_Multiply,id:895,x:34509,y:32829|A-1316-R,B-892-OUT;n:type:ShaderForge.SFN_Tex2d,id:896,x:36080,y:32174,ptlb:Texture,ptin:_Texture,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-923-UVOUT;n:type:ShaderForge.SFN_Panner,id:923,x:36348,y:32176,spu:0,spv:0.1|DIST-2355-OUT;n:type:ShaderForge.SFN_Add,id:974,x:34130,y:32685|A-1898-OUT,B-1771-OUT;n:type:ShaderForge.SFN_Tex2d,id:1316,x:34783,y:32726,ptlb:Gradient_Texture_Decay,ptin:_Gradient_Texture_Decay,tex:1afc2415b297bab478597580754713c6,ntxv:0,isnm:False|UVIN-1319-OUT;n:type:ShaderForge.SFN_Append,id:1319,x:34976,y:32726|A-1948-OUT,B-1359-OUT;n:type:ShaderForge.SFN_TexCoord,id:1336,x:36165,y:32447,uv:0;n:type:ShaderForge.SFN_Multiply,id:1340,x:35992,y:32527|A-1336-V,B-1342-OUT;n:type:ShaderForge.SFN_Vector1,id:1342,x:36165,y:32606,v1:0;n:type:ShaderForge.SFN_Add,id:1359,x:35685,y:32623|A-1340-OUT,B-2056-OUT;n:type:ShaderForge.SFN_Add,id:1497,x:35685,y:32877|A-896-R,B-1805-OUT;n:type:ShaderForge.SFN_TexCoord,id:1618,x:36390,y:33456,uv:0;n:type:ShaderForge.SFN_Tex2d,id:1620,x:36216,y:33456,ptlb:Gradient_Edge_Fake,ptin:_Gradient_Edge_Fake,tex:90dd325f1880d4c4dbf7511cdec5fd07,ntxv:0,isnm:False|UVIN-1618-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1652,x:33713,y:32948,uv:0;n:type:ShaderForge.SFN_Append,id:1654,x:33517,y:32888|A-1665-OUT,B-1652-V;n:type:ShaderForge.SFN_Tex2d,id:1656,x:33351,y:32888,ptlb:Gradient_Color,ptin:_Gradient_Color,tex:d6b46348e3be7e84c83d9734a828e81a,ntxv:0,isnm:False|UVIN-1654-OUT;n:type:ShaderForge.SFN_Clamp,id:1665,x:33727,y:32782|IN-2108-OUT,MIN-1667-OUT,MAX-1666-OUT;n:type:ShaderForge.SFN_Vector1,id:1666,x:33920,y:32864,v1:0.95;n:type:ShaderForge.SFN_Vector1,id:1667,x:33920,y:32815,v1:0.05;n:type:ShaderForge.SFN_SwitchProperty,id:1771,x:36025,y:33456,ptlb:Edge_Fake,ptin:_Edge_Fake,on:True|A-1772-OUT,B-1620-R;n:type:ShaderForge.SFN_Vector1,id:1772,x:36216,y:33311,v1:0;n:type:ShaderForge.SFN_SwitchProperty,id:1805,x:36007,y:33090,ptlb:Mask,ptin:_Mask,on:True|A-1772-OUT,B-2081-R;n:type:ShaderForge.SFN_SwitchProperty,id:1858,x:33183,y:32811,ptlb:Gradient_Or_Solid_Color,ptin:_Gradient_Or_Solid_Color,on:True|A-890-OUT,B-1656-RGB;n:type:ShaderForge.SFN_Color,id:1876,x:33727,y:32636,ptlb:Solid_Color,ptin:_Solid_Color,glob:False,c1:0.1764706,c2:0.5229208,c3:1,c4:1;n:type:ShaderForge.SFN_SwitchProperty,id:1898,x:34324,y:32810,ptlb:Make_Same_As_Mask,ptin:_Make_Same_As_Mask,on:True|A-1316-R,B-895-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1948,x:35168,y:32795,ptlb:Soft_Texture,ptin:_Soft_Texture,on:False|A-2339-OUT,B-896-R;n:type:ShaderForge.SFN_Slider,id:2056,x:36002,y:32764,ptlb:Decay,ptin:_Decay,min:0.05,cur:0.3,max:0.95;n:type:ShaderForge.SFN_Tex2d,id:2081,x:36216,y:33090,ptlb:Mask_Texture,ptin:_Mask_Texture,tex:595df77cdf0a1b64a9da15f136642fb4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2108,x:33920,y:32685|A-974-OUT,B-2109-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2109,x:34130,y:32844,ptlb:Intensity,ptin:_Intensity,glob:False,v1:1;n:type:ShaderForge.SFN_Subtract,id:2339,x:35495,y:32797|A-2056-OUT,B-1497-OUT;n:type:ShaderForge.SFN_Time,id:2354,x:36697,y:32124;n:type:ShaderForge.SFN_Multiply,id:2355,x:36519,y:32195|A-2354-T,B-2356-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2356,x:36697,y:32271,ptlb:Pan_Speed,ptin:_Pan_Speed,glob:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:2392,x:32982,y:32811|A-1858-OUT,B-2393-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2393,x:33172,y:32960,ptlb:Brightness,ptin:_Brightness,glob:False,v1:1;proporder:2393-2109-2356-1858-1656-1876-896-1316-2056-1805-1898-2081-1771-1620-1948;pass:END;sub:END;*/

Shader "ZFS Shaders/ZFS_Flat_Free" {
    Properties {
        _Brightness ("Brightness", Float ) = 1
        _Intensity ("Intensity", Float ) = 1
        _Pan_Speed ("Pan_Speed", Float ) = 1
        [MaterialToggle] _Gradient_Or_Solid_Color ("Gradient_Or_Solid_Color", Float ) = 0.9647059
        _Gradient_Color ("Gradient_Color", 2D) = "white" {}
        _Solid_Color ("Solid_Color", Color) = (0.1764706,0.5229208,1,1)
        _Texture ("Texture", 2D) = "white" {}
        _Gradient_Texture_Decay ("Gradient_Texture_Decay", 2D) = "white" {}
        _Decay ("Decay", Range(0.05, 0.95)) = 0.3
        [MaterialToggle] _Mask ("Mask", Float ) = 1
        [MaterialToggle] _Make_Same_As_Mask ("Make_Same_As_Mask", Float ) = 1
        _Mask_Texture ("Mask_Texture", 2D) = "white" {}
        [MaterialToggle] _Edge_Fake ("Edge_Fake", Float ) = 0
        _Gradient_Edge_Fake ("Gradient_Edge_Fake", 2D) = "white" {}
        [MaterialToggle] _Soft_Texture ("Soft_Texture", Float ) = -1.398039
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
            uniform sampler2D _Gradient_Texture_Decay; uniform float4 _Gradient_Texture_Decay_ST;
            uniform sampler2D _Gradient_Edge_Fake; uniform float4 _Gradient_Edge_Fake_ST;
            uniform sampler2D _Gradient_Color; uniform float4 _Gradient_Color_ST;
            uniform fixed _Edge_Fake;
            uniform fixed _Mask;
            uniform fixed _Gradient_Or_Solid_Color;
            uniform float4 _Solid_Color;
            uniform fixed _Make_Same_As_Mask;
            uniform fixed _Soft_Texture;
            uniform float _Decay;
            uniform sampler2D _Mask_Texture; uniform float4 _Mask_Texture_ST;
            uniform float _Intensity;
            uniform float _Pan_Speed;
            uniform float _Brightness;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
                return o;
            }
            fixed4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_2354 = _Time + _TimeEditor;
                float2 node_2418 = i.uv0;
                float2 node_923 = (node_2418.rg+(node_2354.g*_Pan_Speed)*float2(0,0.1));
                float4 node_896 = tex2D(_Texture,TRANSFORM_TEX(node_923, _Texture));
                float node_1772 = 0.0;
                float node_1805 = lerp( node_1772, tex2D(_Mask_Texture,TRANSFORM_TEX(node_2418.rg, _Mask_Texture)).r, _Mask );
                float2 node_1319 = float2(lerp( (_Decay-(node_896.r+node_1805)), node_896.r, _Soft_Texture ),((i.uv0.g*0.0)+_Decay));
                float4 node_1316 = tex2D(_Gradient_Texture_Decay,TRANSFORM_TEX(node_1319, _Gradient_Texture_Decay));
                float2 node_1618 = i.uv0;
                float node_1771 = lerp( node_1772, tex2D(_Gradient_Edge_Fake,TRANSFORM_TEX(node_1618.rg, _Gradient_Edge_Fake)).r, _Edge_Fake );
                float node_1665 = clamp(((lerp( node_1316.r, (node_1316.r*(node_1805+node_1771)), _Make_Same_As_Mask )+node_1771)*_Intensity),0.05,0.95);
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
