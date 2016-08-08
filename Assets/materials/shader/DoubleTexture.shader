// Compiled shader for Android, uncompressed size: 110.8KB

// Skipping shader variants that would not be included into build of current scene.

Shader "Mobile/Bumped Diffuse Doble" {
Properties {
 _MainTex ("Base (RGB)", 2D) = "white" { }
 _MainTex_2 ("Base (RGB)", 2D) = "white" { }
 _BumpMap ("Normalmap", 2D) = "bump" { }
}
SubShader { 
 LOD 250
 Tags { "RenderType"="Opaque" }


 // Stats for Vertex shader:
 //        gles : 15 avg math (12..17), 2 avg texture (2..3)
 Pass {
  Name "FORWARD"
  Tags { "LIGHTMODE"="ForwardBase" "SHADOWSUPPORT"="true" "RenderType"="Opaque" }
  GpuProgramID 60669
Program "vp" {
SubProgram "gles " {
// Stats: 12 math, 2 textures
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"#version 100

#ifdef VERTEX
attribute vec4 _glesTANGENT;
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_WorldTransformParams;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
void main ()
{
  lowp float tangentSign_1;
  lowp vec3 worldTangent_2;
  lowp vec3 worldNormal_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (_Object2World * _glesVertex).xyz;
  highp vec4 v_5;
  v_5.x = _World2Object[0].x;
  v_5.y = _World2Object[1].x;
  v_5.z = _World2Object[2].x;
  v_5.w = _World2Object[3].x;
  highp vec4 v_6;
  v_6.x = _World2Object[0].y;
  v_6.y = _World2Object[1].y;
  v_6.z = _World2Object[2].y;
  v_6.w = _World2Object[3].y;
  highp vec4 v_7;
  v_7.x = _World2Object[0].z;
  v_7.y = _World2Object[1].z;
  v_7.z = _World2Object[2].z;
  v_7.w = _World2Object[3].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = normalize(((
    (v_5.xyz * _glesNormal.x)
   + 
    (v_6.xyz * _glesNormal.y)
  ) + (v_7.xyz * _glesNormal.z)));
  worldNormal_3 = tmpvar_8;
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = normalize((tmpvar_9 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_10;
  highp float tmpvar_11;
  tmpvar_11 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_11;
  lowp vec3 tmpvar_12;
  tmpvar_12 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_13;
  tmpvar_13.x = worldTangent_2.x;
  tmpvar_13.y = tmpvar_12.x;
  tmpvar_13.z = worldNormal_3.x;
  tmpvar_13.w = tmpvar_4.x;
  highp vec4 tmpvar_14;
  tmpvar_14.x = worldTangent_2.y;
  tmpvar_14.y = tmpvar_12.y;
  tmpvar_14.z = worldNormal_3.y;
  tmpvar_14.w = tmpvar_4.y;
  highp vec4 tmpvar_15;
  tmpvar_15.x = worldTangent_2.z;
  tmpvar_15.y = tmpvar_12.z;
  tmpvar_15.z = worldNormal_3.z;
  tmpvar_15.w = tmpvar_4.z;
  mediump vec3 normal_16;
  normal_16 = worldNormal_3;
  mediump vec4 tmpvar_17;
  tmpvar_17.w = 1.0;
  tmpvar_17.xyz = normal_16;
  mediump vec3 res_18;
  mediump vec3 x_19;
  x_19.x = dot (unity_SHAr, tmpvar_17);
  x_19.y = dot (unity_SHAg, tmpvar_17);
  x_19.z = dot (unity_SHAb, tmpvar_17);
  mediump vec3 x1_20;
  mediump vec4 tmpvar_21;
  tmpvar_21 = (normal_16.xyzz * normal_16.yzzx);
  x1_20.x = dot (unity_SHBr, tmpvar_21);
  x1_20.y = dot (unity_SHBg, tmpvar_21);
  x1_20.z = dot (unity_SHBb, tmpvar_21);
  res_18 = (x_19 + (x1_20 + (unity_SHC.xyz * 
    ((normal_16.x * normal_16.x) - (normal_16.y * normal_16.y))
  )));
  res_18 = max (((1.055 * 
    pow (max (res_18, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_13;
  xlv_TEXCOORD2 = tmpvar_14;
  xlv_TEXCOORD3 = tmpvar_15;
  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), res_18);
}


#endif
#ifdef FRAGMENT
uniform mediump vec4 _WorldSpaceLightPos0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform sampler2D _MainTex_2;
uniform sampler2D _BumpMap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
void main ()
{
  mediump vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  lowp vec3 worldN_3;
  lowp vec4 c_4;
  lowp vec3 lightDir_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = _WorldSpaceLightPos0.xyz;
  lightDir_5 = tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec3 tmpvar_8;
  tmpvar_8 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
  highp float tmpvar_9;
  tmpvar_9 = dot (xlv_TEXCOORD1.xyz, tmpvar_8);
  worldN_3.x = tmpvar_9;
  highp float tmpvar_10;
  tmpvar_10 = dot (xlv_TEXCOORD2.xyz, tmpvar_8);
  worldN_3.y = tmpvar_10;
  highp float tmpvar_11;
  tmpvar_11 = dot (xlv_TEXCOORD3.xyz, tmpvar_8);
  worldN_3.z = tmpvar_11;
  tmpvar_1 = _LightColor0.xyz;
  tmpvar_2 = lightDir_5;
  lowp vec4 c_12;
  lowp vec4 c_13;
  lowp float diff_14;
  mediump float tmpvar_15;
  tmpvar_15 = max (0.0, dot (worldN_3, tmpvar_2));
  diff_14 = tmpvar_15;
  c_13.xyz = ((tmpvar_7.xyz * tmpvar_1) * diff_14);
  c_13.w = tmpvar_7.w;
  c_12.w = c_13.w;
  c_12.xyz = (c_13.xyz + (tmpvar_7.xyz * xlv_TEXCOORD4));
  c_4.xyz = c_12.xyz;
  c_4.w = 1.0;
  gl_FragData[0] = c_4;
}


#endif
"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"#ifdef VERTEX
#version 300 es
uniform 	mediump vec4 unity_SHAr;
uniform 	mediump vec4 unity_SHAg;
uniform 	mediump vec4 unity_SHAb;
uniform 	mediump vec4 unity_SHBr;
uniform 	mediump vec4 unity_SHBg;
uniform 	mediump vec4 unity_SHBb;
uniform 	mediump vec4 unity_SHC;
uniform 	mat4x4 glstate_matrix_mvp;
uniform 	mat4x4 _Object2World;
uniform 	mat4x4 _World2Object;
uniform 	vec4 unity_WorldTransformParams;
uniform 	vec4 _MainTex_ST;
in highp vec4 in_POSITION0;
in highp vec4 in_TANGENT0;
in highp vec3 in_NORMAL0;
in highp vec4 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
out highp vec4 vs_TEXCOORD1;
out highp vec4 vs_TEXCOORD2;
out highp vec4 vs_TEXCOORD3;
out mediump vec3 vs_TEXCOORD4;
vec4 u_xlat0;
vec4 u_xlat1;
mediump vec4 u_xlat16_1;
lowp vec3 u_xlat10_2;
vec3 u_xlat3;
mediump vec3 u_xlat16_4;
mediump vec3 u_xlat16_5;
float u_xlat19;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat0.x = in_NORMAL0.x * _World2Object[0].x;
    u_xlat0.y = in_NORMAL0.x * _World2Object[1].x;
    u_xlat0.zw = in_NORMAL0.xx * _World2Object[2].xx;
    u_xlat1.x = in_NORMAL0.y * _World2Object[0].y;
    u_xlat1.y = in_NORMAL0.y * _World2Object[1].y;
    u_xlat1.zw = in_NORMAL0.yy * _World2Object[2].yy;
    u_xlat0 = u_xlat0 + u_xlat1;
    u_xlat1.x = in_NORMAL0.z * _World2Object[0].z;
    u_xlat1.y = in_NORMAL0.z * _World2Object[1].z;
    u_xlat1.zw = in_NORMAL0.zz * _World2Object[2].zz;
    u_xlat0 = u_xlat0 + u_xlat1;
    u_xlat1.x = dot(u_xlat0.xyw, u_xlat0.xyw);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat0 = u_xlat0.xywz * u_xlat1.xxxx;
    u_xlat1.xyz = in_TANGENT0.yyy * _Object2World[1].yzx;
    u_xlat1.xyz = _Object2World[0].yzx * in_TANGENT0.xxx + u_xlat1.xyz;
    u_xlat1.xyz = _Object2World[2].yzx * in_TANGENT0.zzz + u_xlat1.xyz;
    u_xlat19 = dot(u_xlat1.xyz, u_xlat1.xyz);
    u_xlat19 = inversesqrt(u_xlat19);
    u_xlat1.xyz = vec3(u_xlat19) * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.zxy * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.yzx * u_xlat1.yzx + (-u_xlat10_2.xyz);
    u_xlat19 = in_TANGENT0.w * unity_WorldTransformParams.w;
    u_xlat10_2.xyz = vec3(u_xlat19) * u_xlat10_2.xyz;
    vs_TEXCOORD1.y = u_xlat10_2.x;
    u_xlat3.xyz = in_POSITION0.yyy * _Object2World[1].xyz;
    u_xlat3.xyz = _Object2World[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[3].xyz * in_POSITION0.www + u_xlat3.xyz;
    vs_TEXCOORD1.w = u_xlat3.x;
    vs_TEXCOORD1.z = u_xlat0.x;
    vs_TEXCOORD1.x = u_xlat1.z;
    vs_TEXCOORD2.x = u_xlat1.x;
    vs_TEXCOORD3.x = u_xlat1.y;
    vs_TEXCOORD2.w = u_xlat3.y;
    vs_TEXCOORD3.w = u_xlat3.z;
    vs_TEXCOORD2.z = u_xlat0.y;
    vs_TEXCOORD2.y = u_xlat10_2.y;
    vs_TEXCOORD3.y = u_xlat10_2.z;
    vs_TEXCOORD3.z = u_xlat0.z;
    u_xlat16_4.x = u_xlat0.y * u_xlat0.y;
    u_xlat16_4.x = u_xlat0.x * u_xlat0.x + (-u_xlat16_4.x);
    u_xlat16_1 = u_xlat0.yzwx * u_xlat0.xywz;
    u_xlat16_5.x = dot(unity_SHBr, u_xlat16_1);
    u_xlat16_5.y = dot(unity_SHBg, u_xlat16_1);
    u_xlat16_5.z = dot(unity_SHBb, u_xlat16_1);
    u_xlat16_4.xyz = unity_SHC.xyz * u_xlat16_4.xxx + u_xlat16_5.xyz;
    u_xlat0.w = 1.0;
    u_xlat16_5.x = dot(unity_SHAr, u_xlat0);
    u_xlat16_5.y = dot(unity_SHAg, u_xlat0);
    u_xlat16_5.z = dot(unity_SHAb, u_xlat0);
    u_xlat16_4.xyz = u_xlat16_4.xyz + u_xlat16_5.xyz;
    u_xlat16_4.xyz = max(u_xlat16_4.xyz, vec3(0.0, 0.0, 0.0));
    u_xlat0.xyz = log2(u_xlat16_4.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
    u_xlat0.xyz = exp2(u_xlat0.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
    vs_TEXCOORD4.xyz = u_xlat0.xyz;
    return;
}
#endif
#ifdef FRAGMENT
#version 300 es
precision highp int;
uniform 	mediump vec4 _WorldSpaceLightPos0;
uniform 	lowp vec4 _LightColor0;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _BumpMap;
in highp vec2 vs_TEXCOORD0;
in highp vec4 vs_TEXCOORD1;
in highp vec4 vs_TEXCOORD2;
in highp vec4 vs_TEXCOORD3;
in mediump vec3 vs_TEXCOORD4;
layout(location = 0) out lowp vec4 SV_Target0;
vec3 u_xlat0;
lowp vec3 u_xlat10_0;
lowp vec3 u_xlat10_1;
mediump vec3 u_xlat16_2;
mediump vec3 u_xlat16_3;
mediump vec3 u_xlat16_6;
void main()
{
    u_xlat10_0.xyz = texture(_BumpMap, vs_TEXCOORD0.xy).xyz;
    u_xlat10_1.xyz = u_xlat10_0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
    u_xlat0.x = dot(vs_TEXCOORD1.xyz, u_xlat10_1.xyz);
    u_xlat0.y = dot(vs_TEXCOORD2.xyz, u_xlat10_1.xyz);
    u_xlat0.z = dot(vs_TEXCOORD3.xyz, u_xlat10_1.xyz);
    u_xlat16_2.x = dot(u_xlat0.xyz, _WorldSpaceLightPos0.xyz);
    u_xlat16_2.x = max(u_xlat16_2.x, 0.0);
	vec4 textura2 = texture(_MainTex_2, vs_TEXCOORD0.xy);
    u_xlat10_0.xyz = textura2.a > 0.0 ? textura2.rgb : texture(_MainTex, vs_TEXCOORD0.xy).xyz;
    u_xlat16_6.xyz = u_xlat10_0.xyz * _LightColor0.xyz;
    u_xlat16_3.xyz = u_xlat10_0.xyz * vs_TEXCOORD4.xyz;
    u_xlat16_2.xyz = u_xlat16_6.xyz * u_xlat16_2.xxx + u_xlat16_3.xyz;
    SV_Target0.xyz = u_xlat16_2.xyz;
    SV_Target0.w = 1.0;
    return;
}
#endif
"
}
SubProgram "gles " {
// Stats: 17 math, 3 textures
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"#version 100

#ifdef VERTEX
attribute vec4 _glesTANGENT;
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_WorldTransformParams;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD5;
void main ()
{
  lowp float tangentSign_1;
  lowp vec3 worldTangent_2;
  lowp vec3 worldNormal_3;
  mediump vec4 tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (_Object2World * _glesVertex);
  tmpvar_5 = tmpvar_6.xyz;
  highp vec4 v_7;
  v_7.x = _World2Object[0].x;
  v_7.y = _World2Object[1].x;
  v_7.z = _World2Object[2].x;
  v_7.w = _World2Object[3].x;
  highp vec4 v_8;
  v_8.x = _World2Object[0].y;
  v_8.y = _World2Object[1].y;
  v_8.z = _World2Object[2].y;
  v_8.w = _World2Object[3].y;
  highp vec4 v_9;
  v_9.x = _World2Object[0].z;
  v_9.y = _World2Object[1].z;
  v_9.z = _World2Object[2].z;
  v_9.w = _World2Object[3].z;
  highp vec3 tmpvar_10;
  tmpvar_10 = normalize(((
    (v_7.xyz * _glesNormal.x)
   + 
    (v_8.xyz * _glesNormal.y)
  ) + (v_9.xyz * _glesNormal.z)));
  worldNormal_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = normalize((tmpvar_11 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_12;
  highp float tmpvar_13;
  tmpvar_13 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_13;
  lowp vec3 tmpvar_14;
  tmpvar_14 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_15;
  tmpvar_15.x = worldTangent_2.x;
  tmpvar_15.y = tmpvar_14.x;
  tmpvar_15.z = worldNormal_3.x;
  tmpvar_15.w = tmpvar_5.x;
  highp vec4 tmpvar_16;
  tmpvar_16.x = worldTangent_2.y;
  tmpvar_16.y = tmpvar_14.y;
  tmpvar_16.z = worldNormal_3.y;
  tmpvar_16.w = tmpvar_5.y;
  highp vec4 tmpvar_17;
  tmpvar_17.x = worldTangent_2.z;
  tmpvar_17.y = tmpvar_14.z;
  tmpvar_17.z = worldNormal_3.z;
  tmpvar_17.w = tmpvar_5.z;
  mediump vec3 normal_18;
  normal_18 = worldNormal_3;
  mediump vec4 tmpvar_19;
  tmpvar_19.w = 1.0;
  tmpvar_19.xyz = normal_18;
  mediump vec3 res_20;
  mediump vec3 x_21;
  x_21.x = dot (unity_SHAr, tmpvar_19);
  x_21.y = dot (unity_SHAg, tmpvar_19);
  x_21.z = dot (unity_SHAb, tmpvar_19);
  mediump vec3 x1_22;
  mediump vec4 tmpvar_23;
  tmpvar_23 = (normal_18.xyzz * normal_18.yzzx);
  x1_22.x = dot (unity_SHBr, tmpvar_23);
  x1_22.y = dot (unity_SHBg, tmpvar_23);
  x1_22.z = dot (unity_SHBb, tmpvar_23);
  res_20 = (x_21 + (x1_22 + (unity_SHC.xyz * 
    ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y))
  )));
  res_20 = max (((1.055 * 
    pow (max (res_20, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  tmpvar_4 = (unity_World2Shadow[0] * tmpvar_6);
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_15;
  xlv_TEXCOORD2 = tmpvar_16;
  xlv_TEXCOORD3 = tmpvar_17;
  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), res_20);
  xlv_TEXCOORD5 = tmpvar_4;
}


#endif
#ifdef FRAGMENT
uniform mediump vec4 _WorldSpaceLightPos0;
uniform mediump vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform highp sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD5;
void main ()
{
  mediump float tmpvar_1;
  mediump vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  lowp vec3 worldN_4;
  lowp vec4 c_5;
  lowp vec3 lightDir_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = _WorldSpaceLightPos0.xyz;
  lightDir_6 = tmpvar_7;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
  lowp float tmpvar_10;
  highp float tmpvar_11;
  tmpvar_11 = max (float((texture2DProj (_ShadowMapTexture, xlv_TEXCOORD5).x > 
    (xlv_TEXCOORD5.z / xlv_TEXCOORD5.w)
  )), _LightShadowData.x);
  tmpvar_10 = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
  worldN_4.x = tmpvar_12;
  highp float tmpvar_13;
  tmpvar_13 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
  worldN_4.y = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
  worldN_4.z = tmpvar_14;
  tmpvar_2 = _LightColor0.xyz;
  tmpvar_3 = lightDir_6;
  tmpvar_1 = tmpvar_10;
  mediump vec3 tmpvar_15;
  tmpvar_15 = (tmpvar_2 * tmpvar_1);
  tmpvar_2 = tmpvar_15;
  lowp vec4 c_16;
  lowp vec4 c_17;
  lowp float diff_18;
  mediump float tmpvar_19;
  tmpvar_19 = max (0.0, dot (worldN_4, tmpvar_3));
  diff_18 = tmpvar_19;
  c_17.xyz = ((tmpvar_8.xyz * tmpvar_15) * diff_18);
  c_17.w = tmpvar_8.w;
  c_16.w = c_17.w;
  c_16.xyz = (c_17.xyz + (tmpvar_8.xyz * xlv_TEXCOORD4));
  c_5.xyz = c_16.xyz;
  c_5.w = 1.0;
  gl_FragData[0] = c_5;
}


#endif
"
}
SubProgram "gles " {
// Stats: 12 math, 2 textures
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"#version 100

#ifdef VERTEX
attribute vec4 _glesTANGENT;
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
uniform mediump vec4 unity_4LightAtten0;
uniform mediump vec4 unity_LightColor[8];
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_WorldTransformParams;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
void main ()
{
  lowp float tangentSign_1;
  lowp vec3 worldTangent_2;
  lowp vec3 worldNormal_3;
  mediump vec3 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_Object2World * _glesVertex).xyz;
  highp vec4 v_6;
  v_6.x = _World2Object[0].x;
  v_6.y = _World2Object[1].x;
  v_6.z = _World2Object[2].x;
  v_6.w = _World2Object[3].x;
  highp vec4 v_7;
  v_7.x = _World2Object[0].y;
  v_7.y = _World2Object[1].y;
  v_7.z = _World2Object[2].y;
  v_7.w = _World2Object[3].y;
  highp vec4 v_8;
  v_8.x = _World2Object[0].z;
  v_8.y = _World2Object[1].z;
  v_8.z = _World2Object[2].z;
  v_8.w = _World2Object[3].z;
  highp vec3 tmpvar_9;
  tmpvar_9 = normalize(((
    (v_6.xyz * _glesNormal.x)
   + 
    (v_7.xyz * _glesNormal.y)
  ) + (v_8.xyz * _glesNormal.z)));
  worldNormal_3 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_12;
  lowp vec3 tmpvar_13;
  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_14;
  tmpvar_14.x = worldTangent_2.x;
  tmpvar_14.y = tmpvar_13.x;
  tmpvar_14.z = worldNormal_3.x;
  tmpvar_14.w = tmpvar_5.x;
  highp vec4 tmpvar_15;
  tmpvar_15.x = worldTangent_2.y;
  tmpvar_15.y = tmpvar_13.y;
  tmpvar_15.z = worldNormal_3.y;
  tmpvar_15.w = tmpvar_5.y;
  highp vec4 tmpvar_16;
  tmpvar_16.x = worldTangent_2.z;
  tmpvar_16.y = tmpvar_13.z;
  tmpvar_16.z = worldNormal_3.z;
  tmpvar_16.w = tmpvar_5.z;
  highp vec3 lightColor0_17;
  lightColor0_17 = unity_LightColor[0].xyz;
  highp vec3 lightColor1_18;
  lightColor1_18 = unity_LightColor[1].xyz;
  highp vec3 lightColor2_19;
  lightColor2_19 = unity_LightColor[2].xyz;
  highp vec3 lightColor3_20;
  lightColor3_20 = unity_LightColor[3].xyz;
  highp vec4 lightAttenSq_21;
  lightAttenSq_21 = unity_4LightAtten0;
  highp vec3 normal_22;
  normal_22 = worldNormal_3;
  highp vec3 col_23;
  highp vec4 ndotl_24;
  highp vec4 lengthSq_25;
  highp vec4 tmpvar_26;
  tmpvar_26 = (unity_4LightPosX0 - tmpvar_5.x);
  highp vec4 tmpvar_27;
  tmpvar_27 = (unity_4LightPosY0 - tmpvar_5.y);
  highp vec4 tmpvar_28;
  tmpvar_28 = (unity_4LightPosZ0 - tmpvar_5.z);
  lengthSq_25 = (tmpvar_26 * tmpvar_26);
  lengthSq_25 = (lengthSq_25 + (tmpvar_27 * tmpvar_27));
  lengthSq_25 = (lengthSq_25 + (tmpvar_28 * tmpvar_28));
  ndotl_24 = (tmpvar_26 * normal_22.x);
  ndotl_24 = (ndotl_24 + (tmpvar_27 * normal_22.y));
  ndotl_24 = (ndotl_24 + (tmpvar_28 * normal_22.z));
  highp vec4 tmpvar_29;
  tmpvar_29 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_24 * inversesqrt(lengthSq_25)));
  ndotl_24 = tmpvar_29;
  highp vec4 tmpvar_30;
  tmpvar_30 = (tmpvar_29 * (1.0/((1.0 + 
    (lengthSq_25 * lightAttenSq_21)
  ))));
  col_23 = (lightColor0_17 * tmpvar_30.x);
  col_23 = (col_23 + (lightColor1_18 * tmpvar_30.y));
  col_23 = (col_23 + (lightColor2_19 * tmpvar_30.z));
  col_23 = (col_23 + (lightColor3_20 * tmpvar_30.w));
  tmpvar_4 = col_23;
  mediump vec3 normal_31;
  normal_31 = worldNormal_3;
  mediump vec3 ambient_32;
  mediump vec4 tmpvar_33;
  tmpvar_33.w = 1.0;
  tmpvar_33.xyz = normal_31;
  mediump vec3 res_34;
  mediump vec3 x_35;
  x_35.x = dot (unity_SHAr, tmpvar_33);
  x_35.y = dot (unity_SHAg, tmpvar_33);
  x_35.z = dot (unity_SHAb, tmpvar_33);
  mediump vec3 x1_36;
  mediump vec4 tmpvar_37;
  tmpvar_37 = (normal_31.xyzz * normal_31.yzzx);
  x1_36.x = dot (unity_SHBr, tmpvar_37);
  x1_36.y = dot (unity_SHBg, tmpvar_37);
  x1_36.z = dot (unity_SHBb, tmpvar_37);
  res_34 = (x_35 + (x1_36 + (unity_SHC.xyz * 
    ((normal_31.x * normal_31.x) - (normal_31.y * normal_31.y))
  )));
  res_34 = max (((1.055 * 
    pow (max (res_34, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  ambient_32 = (tmpvar_4 + max (vec3(0.0, 0.0, 0.0), res_34));
  tmpvar_4 = ambient_32;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_14;
  xlv_TEXCOORD2 = tmpvar_15;
  xlv_TEXCOORD3 = tmpvar_16;
  xlv_TEXCOORD4 = ambient_32;
}


#endif
#ifdef FRAGMENT
uniform mediump vec4 _WorldSpaceLightPos0;
uniform lowp vec4 _LightColor0;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
void main ()
{
  mediump vec3 tmpvar_1;
  mediump vec3 tmpvar_2;
  lowp vec3 worldN_3;
  lowp vec4 c_4;
  lowp vec3 lightDir_5;
  mediump vec3 tmpvar_6;
  tmpvar_6 = _WorldSpaceLightPos0.xyz;
  lightDir_5 = tmpvar_6;
  lowp vec4 tmpvar_7;
  tmpvar_7 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec3 tmpvar_8;
  tmpvar_8 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
  highp float tmpvar_9;
  tmpvar_9 = dot (xlv_TEXCOORD1.xyz, tmpvar_8);
  worldN_3.x = tmpvar_9;
  highp float tmpvar_10;
  tmpvar_10 = dot (xlv_TEXCOORD2.xyz, tmpvar_8);
  worldN_3.y = tmpvar_10;
  highp float tmpvar_11;
  tmpvar_11 = dot (xlv_TEXCOORD3.xyz, tmpvar_8);
  worldN_3.z = tmpvar_11;
  tmpvar_1 = _LightColor0.xyz;
  tmpvar_2 = lightDir_5;
  lowp vec4 c_12;
  lowp vec4 c_13;
  lowp float diff_14;
  mediump float tmpvar_15;
  tmpvar_15 = max (0.0, dot (worldN_3, tmpvar_2));
  diff_14 = tmpvar_15;
  c_13.xyz = ((tmpvar_7.xyz * tmpvar_1) * diff_14);
  c_13.w = tmpvar_7.w;
  c_12.w = c_13.w;
  c_12.xyz = (c_13.xyz + (tmpvar_7.xyz * xlv_TEXCOORD4));
  c_4.xyz = c_12.xyz;
  c_4.w = 1.0;
  gl_FragData[0] = c_4;
}


#endif
"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"#ifdef VERTEX
#version 300 es
uniform 	vec4 unity_4LightPosX0;
uniform 	vec4 unity_4LightPosY0;
uniform 	vec4 unity_4LightPosZ0;
uniform 	mediump vec4 unity_4LightAtten0;
uniform 	mediump vec4 unity_LightColor[8];
uniform 	mediump vec4 unity_SHAr;
uniform 	mediump vec4 unity_SHAg;
uniform 	mediump vec4 unity_SHAb;
uniform 	mediump vec4 unity_SHBr;
uniform 	mediump vec4 unity_SHBg;
uniform 	mediump vec4 unity_SHBb;
uniform 	mediump vec4 unity_SHC;
uniform 	mat4x4 glstate_matrix_mvp;
uniform 	mat4x4 _Object2World;
uniform 	mat4x4 _World2Object;
uniform 	vec4 unity_WorldTransformParams;
uniform 	vec4 _MainTex_ST;
in highp vec4 in_POSITION0;
in highp vec4 in_TANGENT0;
in highp vec3 in_NORMAL0;
in highp vec4 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
out highp vec4 vs_TEXCOORD1;
out highp vec4 vs_TEXCOORD2;
out highp vec4 vs_TEXCOORD3;
out mediump vec3 vs_TEXCOORD4;
vec4 u_xlat0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec4 u_xlat16_2;
vec4 u_xlat3;
lowp vec3 u_xlat10_3;
vec4 u_xlat4;
mediump vec3 u_xlat16_5;
mediump vec3 u_xlat16_6;
vec3 u_xlat7;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat0.x = in_TANGENT0.w * unity_WorldTransformParams.w;
    u_xlat7.xyz = in_TANGENT0.yyy * _Object2World[1].yzx;
    u_xlat7.xyz = _Object2World[0].yzx * in_TANGENT0.xxx + u_xlat7.xyz;
    u_xlat7.xyz = _Object2World[2].yzx * in_TANGENT0.zzz + u_xlat7.xyz;
    u_xlat1.x = dot(u_xlat7.xyz, u_xlat7.xyz);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat7.xyz = u_xlat7.xyz * u_xlat1.xxx;
    u_xlat1.x = in_NORMAL0.x * _World2Object[0].x;
    u_xlat1.y = in_NORMAL0.x * _World2Object[1].x;
    u_xlat1.zw = in_NORMAL0.xx * _World2Object[2].xx;
    u_xlat2.x = in_NORMAL0.y * _World2Object[0].y;
    u_xlat2.y = in_NORMAL0.y * _World2Object[1].y;
    u_xlat2.zw = in_NORMAL0.yy * _World2Object[2].yy;
    u_xlat1 = u_xlat1 + u_xlat2;
    u_xlat2.x = in_NORMAL0.z * _World2Object[0].z;
    u_xlat2.y = in_NORMAL0.z * _World2Object[1].z;
    u_xlat2.zw = in_NORMAL0.zz * _World2Object[2].zz;
    u_xlat1 = u_xlat1 + u_xlat2;
    u_xlat2.x = dot(u_xlat1.xyw, u_xlat1.xyw);
    u_xlat2.x = inversesqrt(u_xlat2.x);
    u_xlat1 = u_xlat1.xywz * u_xlat2.xxxx;
    u_xlat10_3.xyz = u_xlat7.xyz * u_xlat1.zxy;
    u_xlat10_3.xyz = u_xlat1.yzx * u_xlat7.yzx + (-u_xlat10_3.xyz);
    u_xlat10_3.xyz = u_xlat0.xxx * u_xlat10_3.xyz;
    vs_TEXCOORD1.y = u_xlat10_3.x;
    vs_TEXCOORD1.x = u_xlat7.z;
    vs_TEXCOORD1.z = u_xlat1.x;
    u_xlat2.xyz = in_POSITION0.yyy * _Object2World[1].xyz;
    u_xlat2.xyz = _Object2World[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
    u_xlat2.xyz = _Object2World[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
    u_xlat2.xyz = _Object2World[3].xyz * in_POSITION0.www + u_xlat2.xyz;
    vs_TEXCOORD1.w = u_xlat2.x;
    vs_TEXCOORD2.x = u_xlat7.x;
    vs_TEXCOORD3.x = u_xlat7.y;
    vs_TEXCOORD2.y = u_xlat10_3.y;
    vs_TEXCOORD3.y = u_xlat10_3.z;
    vs_TEXCOORD2.z = u_xlat1.y;
    vs_TEXCOORD2.w = u_xlat2.y;
    vs_TEXCOORD3.z = u_xlat1.z;
    vs_TEXCOORD3.w = u_xlat2.z;
    u_xlat0 = (-u_xlat2.yyyy) + unity_4LightPosY0;
    u_xlat3 = u_xlat1.yyyy * u_xlat0;
    u_xlat0 = u_xlat0 * u_xlat0;
    u_xlat4 = (-u_xlat2.xxxx) + unity_4LightPosX0;
    u_xlat2 = (-u_xlat2.zzzz) + unity_4LightPosZ0;
    u_xlat3 = u_xlat4 * u_xlat1.xxxx + u_xlat3;
    u_xlat0 = u_xlat4 * u_xlat4 + u_xlat0;
    u_xlat0 = u_xlat2 * u_xlat2 + u_xlat0;
    u_xlat2 = u_xlat2 * u_xlat1.zzwz + u_xlat3;
    u_xlat3 = inversesqrt(u_xlat0);
    u_xlat0 = u_xlat0 * unity_4LightAtten0 + vec4(1.0, 1.0, 1.0, 1.0);
    u_xlat0 = vec4(1.0, 1.0, 1.0, 1.0) / u_xlat0;
    u_xlat2 = u_xlat2 * u_xlat3;
    u_xlat2 = max(u_xlat2, vec4(0.0, 0.0, 0.0, 0.0));
    u_xlat0 = u_xlat0 * u_xlat2;
    u_xlat2.xyz = u_xlat0.yyy * unity_LightColor[1].xyz;
    u_xlat2.xyz = unity_LightColor[0].xyz * u_xlat0.xxx + u_xlat2.xyz;
    u_xlat0.xyz = unity_LightColor[2].xyz * u_xlat0.zzz + u_xlat2.xyz;
    u_xlat0.xyz = unity_LightColor[3].xyz * u_xlat0.www + u_xlat0.xyz;
    u_xlat16_5.x = u_xlat1.y * u_xlat1.y;
    u_xlat16_5.x = u_xlat1.x * u_xlat1.x + (-u_xlat16_5.x);
    u_xlat16_2 = u_xlat1.yzwx * u_xlat1.xywz;
    u_xlat16_6.x = dot(unity_SHBr, u_xlat16_2);
    u_xlat16_6.y = dot(unity_SHBg, u_xlat16_2);
    u_xlat16_6.z = dot(unity_SHBb, u_xlat16_2);
    u_xlat16_5.xyz = unity_SHC.xyz * u_xlat16_5.xxx + u_xlat16_6.xyz;
    u_xlat1.w = 1.0;
    u_xlat16_6.x = dot(unity_SHAr, u_xlat1);
    u_xlat16_6.y = dot(unity_SHAg, u_xlat1);
    u_xlat16_6.z = dot(unity_SHAb, u_xlat1);
    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
    u_xlat16_5.xyz = max(u_xlat16_5.xyz, vec3(0.0, 0.0, 0.0));
    u_xlat1.xyz = log2(u_xlat16_5.xyz);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
    u_xlat1.xyz = exp2(u_xlat1.xyz);
    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.0, 0.0, 0.0));
    vs_TEXCOORD4.xyz = u_xlat0.xyz + u_xlat1.xyz;
    return;
}
#endif
#ifdef FRAGMENT
#version 300 es
precision highp int;
uniform 	mediump vec4 _WorldSpaceLightPos0;
uniform 	lowp vec4 _LightColor0;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _BumpMap;
in highp vec2 vs_TEXCOORD0;
in highp vec4 vs_TEXCOORD1;
in highp vec4 vs_TEXCOORD2;
in highp vec4 vs_TEXCOORD3;
in mediump vec3 vs_TEXCOORD4;
layout(location = 0) out lowp vec4 SV_Target0;
vec3 u_xlat0;
lowp vec3 u_xlat10_0;
lowp vec3 u_xlat10_1;
mediump vec3 u_xlat16_2;
mediump vec3 u_xlat16_3;
mediump vec3 u_xlat16_6;
void main()
{
    u_xlat10_0.xyz = texture(_BumpMap, vs_TEXCOORD0.xy).xyz;
    u_xlat10_1.xyz = u_xlat10_0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
    u_xlat0.x = dot(vs_TEXCOORD1.xyz, u_xlat10_1.xyz);
    u_xlat0.y = dot(vs_TEXCOORD2.xyz, u_xlat10_1.xyz);
    u_xlat0.z = dot(vs_TEXCOORD3.xyz, u_xlat10_1.xyz);
    u_xlat16_2.x = dot(u_xlat0.xyz, _WorldSpaceLightPos0.xyz);
    u_xlat16_2.x = max(u_xlat16_2.x, 0.0);
    u_xlat10_0.xyz = texture(_MainTex, vs_TEXCOORD0.xy).xyz;
    u_xlat16_6.xyz = u_xlat10_0.xyz * _LightColor0.xyz;
    u_xlat16_3.xyz = u_xlat10_0.xyz * vs_TEXCOORD4.xyz;
    u_xlat16_2.xyz = u_xlat16_6.xyz * u_xlat16_2.xxx + u_xlat16_3.xyz;
    SV_Target0.xyz = u_xlat16_2.xyz;
    SV_Target0.w = 1.0;
    return;
}
#endif
"
}
SubProgram "gles " {
// Stats: 17 math, 3 textures
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"#version 100

#ifdef VERTEX
attribute vec4 _glesTANGENT;
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
uniform mediump vec4 unity_4LightAtten0;
uniform mediump vec4 unity_LightColor[8];
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_WorldTransformParams;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD5;
void main ()
{
  lowp float tangentSign_1;
  lowp vec3 worldTangent_2;
  lowp vec3 worldNormal_3;
  mediump vec3 tmpvar_4;
  mediump vec4 tmpvar_5;
  highp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7 = (_Object2World * _glesVertex);
  tmpvar_6 = tmpvar_7.xyz;
  highp vec4 v_8;
  v_8.x = _World2Object[0].x;
  v_8.y = _World2Object[1].x;
  v_8.z = _World2Object[2].x;
  v_8.w = _World2Object[3].x;
  highp vec4 v_9;
  v_9.x = _World2Object[0].y;
  v_9.y = _World2Object[1].y;
  v_9.z = _World2Object[2].y;
  v_9.w = _World2Object[3].y;
  highp vec4 v_10;
  v_10.x = _World2Object[0].z;
  v_10.y = _World2Object[1].z;
  v_10.z = _World2Object[2].z;
  v_10.w = _World2Object[3].z;
  highp vec3 tmpvar_11;
  tmpvar_11 = normalize(((
    (v_8.xyz * _glesNormal.x)
   + 
    (v_9.xyz * _glesNormal.y)
  ) + (v_10.xyz * _glesNormal.z)));
  worldNormal_3 = tmpvar_11;
  highp mat3 tmpvar_12;
  tmpvar_12[0] = _Object2World[0].xyz;
  tmpvar_12[1] = _Object2World[1].xyz;
  tmpvar_12[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_13;
  tmpvar_13 = normalize((tmpvar_12 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_14;
  lowp vec3 tmpvar_15;
  tmpvar_15 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_16;
  tmpvar_16.x = worldTangent_2.x;
  tmpvar_16.y = tmpvar_15.x;
  tmpvar_16.z = worldNormal_3.x;
  tmpvar_16.w = tmpvar_6.x;
  highp vec4 tmpvar_17;
  tmpvar_17.x = worldTangent_2.y;
  tmpvar_17.y = tmpvar_15.y;
  tmpvar_17.z = worldNormal_3.y;
  tmpvar_17.w = tmpvar_6.y;
  highp vec4 tmpvar_18;
  tmpvar_18.x = worldTangent_2.z;
  tmpvar_18.y = tmpvar_15.z;
  tmpvar_18.z = worldNormal_3.z;
  tmpvar_18.w = tmpvar_6.z;
  highp vec3 lightColor0_19;
  lightColor0_19 = unity_LightColor[0].xyz;
  highp vec3 lightColor1_20;
  lightColor1_20 = unity_LightColor[1].xyz;
  highp vec3 lightColor2_21;
  lightColor2_21 = unity_LightColor[2].xyz;
  highp vec3 lightColor3_22;
  lightColor3_22 = unity_LightColor[3].xyz;
  highp vec4 lightAttenSq_23;
  lightAttenSq_23 = unity_4LightAtten0;
  highp vec3 normal_24;
  normal_24 = worldNormal_3;
  highp vec3 col_25;
  highp vec4 ndotl_26;
  highp vec4 lengthSq_27;
  highp vec4 tmpvar_28;
  tmpvar_28 = (unity_4LightPosX0 - tmpvar_7.x);
  highp vec4 tmpvar_29;
  tmpvar_29 = (unity_4LightPosY0 - tmpvar_7.y);
  highp vec4 tmpvar_30;
  tmpvar_30 = (unity_4LightPosZ0 - tmpvar_7.z);
  lengthSq_27 = (tmpvar_28 * tmpvar_28);
  lengthSq_27 = (lengthSq_27 + (tmpvar_29 * tmpvar_29));
  lengthSq_27 = (lengthSq_27 + (tmpvar_30 * tmpvar_30));
  ndotl_26 = (tmpvar_28 * normal_24.x);
  ndotl_26 = (ndotl_26 + (tmpvar_29 * normal_24.y));
  ndotl_26 = (ndotl_26 + (tmpvar_30 * normal_24.z));
  highp vec4 tmpvar_31;
  tmpvar_31 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_26 * inversesqrt(lengthSq_27)));
  ndotl_26 = tmpvar_31;
  highp vec4 tmpvar_32;
  tmpvar_32 = (tmpvar_31 * (1.0/((1.0 + 
    (lengthSq_27 * lightAttenSq_23)
  ))));
  col_25 = (lightColor0_19 * tmpvar_32.x);
  col_25 = (col_25 + (lightColor1_20 * tmpvar_32.y));
  col_25 = (col_25 + (lightColor2_21 * tmpvar_32.z));
  col_25 = (col_25 + (lightColor3_22 * tmpvar_32.w));
  tmpvar_4 = col_25;
  mediump vec3 normal_33;
  normal_33 = worldNormal_3;
  mediump vec3 ambient_34;
  mediump vec4 tmpvar_35;
  tmpvar_35.w = 1.0;
  tmpvar_35.xyz = normal_33;
  mediump vec3 res_36;
  mediump vec3 x_37;
  x_37.x = dot (unity_SHAr, tmpvar_35);
  x_37.y = dot (unity_SHAg, tmpvar_35);
  x_37.z = dot (unity_SHAb, tmpvar_35);
  mediump vec3 x1_38;
  mediump vec4 tmpvar_39;
  tmpvar_39 = (normal_33.xyzz * normal_33.yzzx);
  x1_38.x = dot (unity_SHBr, tmpvar_39);
  x1_38.y = dot (unity_SHBg, tmpvar_39);
  x1_38.z = dot (unity_SHBb, tmpvar_39);
  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
    ((normal_33.x * normal_33.x) - (normal_33.y * normal_33.y))
  )));
  res_36 = max (((1.055 * 
    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  ambient_34 = (tmpvar_4 + max (vec3(0.0, 0.0, 0.0), res_36));
  tmpvar_4 = ambient_34;
  tmpvar_5 = (unity_World2Shadow[0] * tmpvar_7);
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_16;
  xlv_TEXCOORD2 = tmpvar_17;
  xlv_TEXCOORD3 = tmpvar_18;
  xlv_TEXCOORD4 = ambient_34;
  xlv_TEXCOORD5 = tmpvar_5;
}


#endif
#ifdef FRAGMENT
uniform mediump vec4 _WorldSpaceLightPos0;
uniform mediump vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform highp sampler2D _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD5;
void main ()
{
  mediump float tmpvar_1;
  mediump vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  lowp vec3 worldN_4;
  lowp vec4 c_5;
  lowp vec3 lightDir_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = _WorldSpaceLightPos0.xyz;
  lightDir_6 = tmpvar_7;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
  lowp float tmpvar_10;
  highp float tmpvar_11;
  tmpvar_11 = max (float((texture2DProj (_ShadowMapTexture, xlv_TEXCOORD5).x > 
    (xlv_TEXCOORD5.z / xlv_TEXCOORD5.w)
  )), _LightShadowData.x);
  tmpvar_10 = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
  worldN_4.x = tmpvar_12;
  highp float tmpvar_13;
  tmpvar_13 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
  worldN_4.y = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
  worldN_4.z = tmpvar_14;
  tmpvar_2 = _LightColor0.xyz;
  tmpvar_3 = lightDir_6;
  tmpvar_1 = tmpvar_10;
  mediump vec3 tmpvar_15;
  tmpvar_15 = (tmpvar_2 * tmpvar_1);
  tmpvar_2 = tmpvar_15;
  lowp vec4 c_16;
  lowp vec4 c_17;
  lowp float diff_18;
  mediump float tmpvar_19;
  tmpvar_19 = max (0.0, dot (worldN_4, tmpvar_3));
  diff_18 = tmpvar_19;
  c_17.xyz = ((tmpvar_8.xyz * tmpvar_15) * diff_18);
  c_17.w = tmpvar_8.w;
  c_16.w = c_17.w;
  c_16.xyz = (c_17.xyz + (tmpvar_8.xyz * xlv_TEXCOORD4));
  c_5.xyz = c_16.xyz;
  c_5.w = 1.0;
  gl_FragData[0] = c_5;
}


#endif
"
}
SubProgram "gles " {
// Stats: 16 math, 3 textures
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"#version 100

#ifdef VERTEX
#extension GL_EXT_shadow_samplers : enable
attribute vec4 _glesTANGENT;
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_WorldTransformParams;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD5;
void main ()
{
  lowp float tangentSign_1;
  lowp vec3 worldTangent_2;
  lowp vec3 worldNormal_3;
  mediump vec4 tmpvar_4;
  highp vec3 tmpvar_5;
  highp vec4 tmpvar_6;
  tmpvar_6 = (_Object2World * _glesVertex);
  tmpvar_5 = tmpvar_6.xyz;
  highp vec4 v_7;
  v_7.x = _World2Object[0].x;
  v_7.y = _World2Object[1].x;
  v_7.z = _World2Object[2].x;
  v_7.w = _World2Object[3].x;
  highp vec4 v_8;
  v_8.x = _World2Object[0].y;
  v_8.y = _World2Object[1].y;
  v_8.z = _World2Object[2].y;
  v_8.w = _World2Object[3].y;
  highp vec4 v_9;
  v_9.x = _World2Object[0].z;
  v_9.y = _World2Object[1].z;
  v_9.z = _World2Object[2].z;
  v_9.w = _World2Object[3].z;
  highp vec3 tmpvar_10;
  tmpvar_10 = normalize(((
    (v_7.xyz * _glesNormal.x)
   + 
    (v_8.xyz * _glesNormal.y)
  ) + (v_9.xyz * _glesNormal.z)));
  worldNormal_3 = tmpvar_10;
  highp mat3 tmpvar_11;
  tmpvar_11[0] = _Object2World[0].xyz;
  tmpvar_11[1] = _Object2World[1].xyz;
  tmpvar_11[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_12;
  tmpvar_12 = normalize((tmpvar_11 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_12;
  highp float tmpvar_13;
  tmpvar_13 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_13;
  lowp vec3 tmpvar_14;
  tmpvar_14 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_15;
  tmpvar_15.x = worldTangent_2.x;
  tmpvar_15.y = tmpvar_14.x;
  tmpvar_15.z = worldNormal_3.x;
  tmpvar_15.w = tmpvar_5.x;
  highp vec4 tmpvar_16;
  tmpvar_16.x = worldTangent_2.y;
  tmpvar_16.y = tmpvar_14.y;
  tmpvar_16.z = worldNormal_3.y;
  tmpvar_16.w = tmpvar_5.y;
  highp vec4 tmpvar_17;
  tmpvar_17.x = worldTangent_2.z;
  tmpvar_17.y = tmpvar_14.z;
  tmpvar_17.z = worldNormal_3.z;
  tmpvar_17.w = tmpvar_5.z;
  mediump vec3 normal_18;
  normal_18 = worldNormal_3;
  mediump vec4 tmpvar_19;
  tmpvar_19.w = 1.0;
  tmpvar_19.xyz = normal_18;
  mediump vec3 res_20;
  mediump vec3 x_21;
  x_21.x = dot (unity_SHAr, tmpvar_19);
  x_21.y = dot (unity_SHAg, tmpvar_19);
  x_21.z = dot (unity_SHAb, tmpvar_19);
  mediump vec3 x1_22;
  mediump vec4 tmpvar_23;
  tmpvar_23 = (normal_18.xyzz * normal_18.yzzx);
  x1_22.x = dot (unity_SHBr, tmpvar_23);
  x1_22.y = dot (unity_SHBg, tmpvar_23);
  x1_22.z = dot (unity_SHBb, tmpvar_23);
  res_20 = (x_21 + (x1_22 + (unity_SHC.xyz * 
    ((normal_18.x * normal_18.x) - (normal_18.y * normal_18.y))
  )));
  res_20 = max (((1.055 * 
    pow (max (res_20, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  tmpvar_4 = (unity_World2Shadow[0] * tmpvar_6);
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_15;
  xlv_TEXCOORD2 = tmpvar_16;
  xlv_TEXCOORD3 = tmpvar_17;
  xlv_TEXCOORD4 = max (vec3(0.0, 0.0, 0.0), res_20);
  xlv_TEXCOORD5 = tmpvar_4;
}


#endif
#ifdef FRAGMENT
#extension GL_EXT_shadow_samplers : enable
uniform mediump vec4 _WorldSpaceLightPos0;
uniform mediump vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD5;
void main ()
{
  mediump float tmpvar_1;
  mediump vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  lowp vec3 worldN_4;
  lowp vec4 c_5;
  lowp vec3 lightDir_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = _WorldSpaceLightPos0.xyz;
  lightDir_6 = tmpvar_7;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
  lowp float shadow_10;
  shadow_10 = (_LightShadowData.x + (shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD5.xyz) * (1.0 - _LightShadowData.x)));
  highp float tmpvar_11;
  tmpvar_11 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
  worldN_4.x = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
  worldN_4.y = tmpvar_12;
  highp float tmpvar_13;
  tmpvar_13 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
  worldN_4.z = tmpvar_13;
  tmpvar_2 = _LightColor0.xyz;
  tmpvar_3 = lightDir_6;
  tmpvar_1 = shadow_10;
  mediump vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_2 * tmpvar_1);
  tmpvar_2 = tmpvar_14;
  lowp vec4 c_15;
  lowp vec4 c_16;
  lowp float diff_17;
  mediump float tmpvar_18;
  tmpvar_18 = max (0.0, dot (worldN_4, tmpvar_3));
  diff_17 = tmpvar_18;
  c_16.xyz = ((tmpvar_8.xyz * tmpvar_14) * diff_17);
  c_16.w = tmpvar_8.w;
  c_15.w = c_16.w;
  c_15.xyz = (c_16.xyz + (tmpvar_8.xyz * xlv_TEXCOORD4));
  c_5.xyz = c_15.xyz;
  c_5.w = 1.0;
  gl_FragData[0] = c_5;
}


#endif
"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"#ifdef VERTEX
#version 300 es
uniform 	mediump vec4 unity_SHAr;
uniform 	mediump vec4 unity_SHAg;
uniform 	mediump vec4 unity_SHAb;
uniform 	mediump vec4 unity_SHBr;
uniform 	mediump vec4 unity_SHBg;
uniform 	mediump vec4 unity_SHBb;
uniform 	mediump vec4 unity_SHC;
uniform 	mat4x4 unity_World2Shadow[4];
uniform 	mat4x4 glstate_matrix_mvp;
uniform 	mat4x4 _Object2World;
uniform 	mat4x4 _World2Object;
uniform 	vec4 unity_WorldTransformParams;
uniform 	vec4 _MainTex_ST;
in highp vec4 in_POSITION0;
in highp vec4 in_TANGENT0;
in highp vec3 in_NORMAL0;
in highp vec4 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
out highp vec4 vs_TEXCOORD1;
out highp vec4 vs_TEXCOORD2;
out highp vec4 vs_TEXCOORD3;
out mediump vec3 vs_TEXCOORD4;
out mediump vec4 vs_TEXCOORD5;
vec4 u_xlat0;
vec4 u_xlat1;
mediump vec4 u_xlat16_1;
lowp vec3 u_xlat10_2;
vec3 u_xlat3;
mediump vec3 u_xlat16_4;
mediump vec3 u_xlat16_5;
float u_xlat19;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat0.x = in_NORMAL0.x * _World2Object[0].x;
    u_xlat0.y = in_NORMAL0.x * _World2Object[1].x;
    u_xlat0.zw = in_NORMAL0.xx * _World2Object[2].xx;
    u_xlat1.x = in_NORMAL0.y * _World2Object[0].y;
    u_xlat1.y = in_NORMAL0.y * _World2Object[1].y;
    u_xlat1.zw = in_NORMAL0.yy * _World2Object[2].yy;
    u_xlat0 = u_xlat0 + u_xlat1;
    u_xlat1.x = in_NORMAL0.z * _World2Object[0].z;
    u_xlat1.y = in_NORMAL0.z * _World2Object[1].z;
    u_xlat1.zw = in_NORMAL0.zz * _World2Object[2].zz;
    u_xlat0 = u_xlat0 + u_xlat1;
    u_xlat1.x = dot(u_xlat0.xyw, u_xlat0.xyw);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat0 = u_xlat0.xywz * u_xlat1.xxxx;
    u_xlat1.xyz = in_TANGENT0.yyy * _Object2World[1].yzx;
    u_xlat1.xyz = _Object2World[0].yzx * in_TANGENT0.xxx + u_xlat1.xyz;
    u_xlat1.xyz = _Object2World[2].yzx * in_TANGENT0.zzz + u_xlat1.xyz;
    u_xlat19 = dot(u_xlat1.xyz, u_xlat1.xyz);
    u_xlat19 = inversesqrt(u_xlat19);
    u_xlat1.xyz = vec3(u_xlat19) * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.zxy * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.yzx * u_xlat1.yzx + (-u_xlat10_2.xyz);
    u_xlat19 = in_TANGENT0.w * unity_WorldTransformParams.w;
    u_xlat10_2.xyz = vec3(u_xlat19) * u_xlat10_2.xyz;
    vs_TEXCOORD1.y = u_xlat10_2.x;
    u_xlat3.xyz = in_POSITION0.yyy * _Object2World[1].xyz;
    u_xlat3.xyz = _Object2World[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[3].xyz * in_POSITION0.www + u_xlat3.xyz;
    vs_TEXCOORD1.w = u_xlat3.x;
    vs_TEXCOORD1.z = u_xlat0.x;
    vs_TEXCOORD1.x = u_xlat1.z;
    vs_TEXCOORD2.x = u_xlat1.x;
    vs_TEXCOORD3.x = u_xlat1.y;
    vs_TEXCOORD2.w = u_xlat3.y;
    vs_TEXCOORD3.w = u_xlat3.z;
    vs_TEXCOORD2.z = u_xlat0.y;
    vs_TEXCOORD2.y = u_xlat10_2.y;
    vs_TEXCOORD3.y = u_xlat10_2.z;
    vs_TEXCOORD3.z = u_xlat0.z;
    u_xlat16_4.x = u_xlat0.y * u_xlat0.y;
    u_xlat16_4.x = u_xlat0.x * u_xlat0.x + (-u_xlat16_4.x);
    u_xlat16_1 = u_xlat0.yzwx * u_xlat0.xywz;
    u_xlat16_5.x = dot(unity_SHBr, u_xlat16_1);
    u_xlat16_5.y = dot(unity_SHBg, u_xlat16_1);
    u_xlat16_5.z = dot(unity_SHBb, u_xlat16_1);
    u_xlat16_4.xyz = unity_SHC.xyz * u_xlat16_4.xxx + u_xlat16_5.xyz;
    u_xlat0.w = 1.0;
    u_xlat16_5.x = dot(unity_SHAr, u_xlat0);
    u_xlat16_5.y = dot(unity_SHAg, u_xlat0);
    u_xlat16_5.z = dot(unity_SHAb, u_xlat0);
    u_xlat16_4.xyz = u_xlat16_4.xyz + u_xlat16_5.xyz;
    u_xlat16_4.xyz = max(u_xlat16_4.xyz, vec3(0.0, 0.0, 0.0));
    u_xlat0.xyz = log2(u_xlat16_4.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
    u_xlat0.xyz = exp2(u_xlat0.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
    vs_TEXCOORD4.xyz = u_xlat0.xyz;
    u_xlat0 = in_POSITION0.yyyy * _Object2World[1];
    u_xlat0 = _Object2World[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = _Object2World[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat0 = _Object2World[3] * in_POSITION0.wwww + u_xlat0;
    u_xlat1 = u_xlat0.yyyy * unity_World2Shadow[0][1];
    u_xlat1 = unity_World2Shadow[0][0] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = unity_World2Shadow[0][2] * u_xlat0.zzzz + u_xlat1;
    u_xlat0 = unity_World2Shadow[0][3] * u_xlat0.wwww + u_xlat1;
    vs_TEXCOORD5 = u_xlat0;
    return;
}
#endif
#ifdef FRAGMENT
#version 300 es
precision highp int;
uniform 	mediump vec4 _WorldSpaceLightPos0;
uniform 	mediump vec4 _LightShadowData;
uniform 	lowp vec4 _LightColor0;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _BumpMap;
uniform lowp sampler2DShadow hlslcc_zcmp_ShadowMapTexture;
uniform lowp sampler2D _ShadowMapTexture;
in highp vec2 vs_TEXCOORD0;
in highp vec4 vs_TEXCOORD1;
in highp vec4 vs_TEXCOORD2;
in highp vec4 vs_TEXCOORD3;
in mediump vec3 vs_TEXCOORD4;
in mediump vec4 vs_TEXCOORD5;
layout(location = 0) out lowp vec4 SV_Target0;
vec3 u_xlat0;
lowp vec3 u_xlat10_0;
lowp vec3 u_xlat10_1;
mediump vec3 u_xlat16_2;
mediump vec3 u_xlat16_3;
mediump vec3 u_xlat16_6;
mediump float u_xlat16_10;
void main()
{
    u_xlat10_0.xyz = texture(_BumpMap, vs_TEXCOORD0.xy).xyz;
    u_xlat10_1.xyz = u_xlat10_0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
    u_xlat0.x = dot(vs_TEXCOORD1.xyz, u_xlat10_1.xyz);
    u_xlat0.y = dot(vs_TEXCOORD2.xyz, u_xlat10_1.xyz);
    u_xlat0.z = dot(vs_TEXCOORD3.xyz, u_xlat10_1.xyz);
    u_xlat16_2.x = dot(u_xlat0.xyz, _WorldSpaceLightPos0.xyz);
    u_xlat16_2.x = max(u_xlat16_2.x, 0.0);
    vec3 txVec0 = vec3(vs_TEXCOORD5.xy,vs_TEXCOORD5.z);
    u_xlat16_6.x = textureLod(hlslcc_zcmp_ShadowMapTexture, txVec0, 0.0);
    u_xlat16_10 = (-_LightShadowData.x) + 1.0;
    u_xlat16_6.x = u_xlat16_6.x * u_xlat16_10 + _LightShadowData.x;
    u_xlat16_6.xyz = u_xlat16_6.xxx * _LightColor0.xyz;
    u_xlat10_0.xyz = texture(_MainTex, vs_TEXCOORD0.xy).xyz;
    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat10_0.xyz;
    u_xlat16_3.xyz = u_xlat10_0.xyz * vs_TEXCOORD4.xyz;
    u_xlat16_2.xyz = u_xlat16_6.xyz * u_xlat16_2.xxx + u_xlat16_3.xyz;
    SV_Target0.xyz = u_xlat16_2.xyz;
    SV_Target0.w = 1.0;
    return;
}
#endif
"
}
SubProgram "gles " {
// Stats: 16 math, 3 textures
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"#version 100

#ifdef VERTEX
#extension GL_EXT_shadow_samplers : enable
attribute vec4 _glesTANGENT;
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 unity_4LightPosX0;
uniform highp vec4 unity_4LightPosY0;
uniform highp vec4 unity_4LightPosZ0;
uniform mediump vec4 unity_4LightAtten0;
uniform mediump vec4 unity_LightColor[8];
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 unity_World2Shadow[4];
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_WorldTransformParams;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD5;
void main ()
{
  lowp float tangentSign_1;
  lowp vec3 worldTangent_2;
  lowp vec3 worldNormal_3;
  mediump vec3 tmpvar_4;
  mediump vec4 tmpvar_5;
  highp vec3 tmpvar_6;
  highp vec4 tmpvar_7;
  tmpvar_7 = (_Object2World * _glesVertex);
  tmpvar_6 = tmpvar_7.xyz;
  highp vec4 v_8;
  v_8.x = _World2Object[0].x;
  v_8.y = _World2Object[1].x;
  v_8.z = _World2Object[2].x;
  v_8.w = _World2Object[3].x;
  highp vec4 v_9;
  v_9.x = _World2Object[0].y;
  v_9.y = _World2Object[1].y;
  v_9.z = _World2Object[2].y;
  v_9.w = _World2Object[3].y;
  highp vec4 v_10;
  v_10.x = _World2Object[0].z;
  v_10.y = _World2Object[1].z;
  v_10.z = _World2Object[2].z;
  v_10.w = _World2Object[3].z;
  highp vec3 tmpvar_11;
  tmpvar_11 = normalize(((
    (v_8.xyz * _glesNormal.x)
   + 
    (v_9.xyz * _glesNormal.y)
  ) + (v_10.xyz * _glesNormal.z)));
  worldNormal_3 = tmpvar_11;
  highp mat3 tmpvar_12;
  tmpvar_12[0] = _Object2World[0].xyz;
  tmpvar_12[1] = _Object2World[1].xyz;
  tmpvar_12[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_13;
  tmpvar_13 = normalize((tmpvar_12 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_13;
  highp float tmpvar_14;
  tmpvar_14 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_14;
  lowp vec3 tmpvar_15;
  tmpvar_15 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_16;
  tmpvar_16.x = worldTangent_2.x;
  tmpvar_16.y = tmpvar_15.x;
  tmpvar_16.z = worldNormal_3.x;
  tmpvar_16.w = tmpvar_6.x;
  highp vec4 tmpvar_17;
  tmpvar_17.x = worldTangent_2.y;
  tmpvar_17.y = tmpvar_15.y;
  tmpvar_17.z = worldNormal_3.y;
  tmpvar_17.w = tmpvar_6.y;
  highp vec4 tmpvar_18;
  tmpvar_18.x = worldTangent_2.z;
  tmpvar_18.y = tmpvar_15.z;
  tmpvar_18.z = worldNormal_3.z;
  tmpvar_18.w = tmpvar_6.z;
  highp vec3 lightColor0_19;
  lightColor0_19 = unity_LightColor[0].xyz;
  highp vec3 lightColor1_20;
  lightColor1_20 = unity_LightColor[1].xyz;
  highp vec3 lightColor2_21;
  lightColor2_21 = unity_LightColor[2].xyz;
  highp vec3 lightColor3_22;
  lightColor3_22 = unity_LightColor[3].xyz;
  highp vec4 lightAttenSq_23;
  lightAttenSq_23 = unity_4LightAtten0;
  highp vec3 normal_24;
  normal_24 = worldNormal_3;
  highp vec3 col_25;
  highp vec4 ndotl_26;
  highp vec4 lengthSq_27;
  highp vec4 tmpvar_28;
  tmpvar_28 = (unity_4LightPosX0 - tmpvar_7.x);
  highp vec4 tmpvar_29;
  tmpvar_29 = (unity_4LightPosY0 - tmpvar_7.y);
  highp vec4 tmpvar_30;
  tmpvar_30 = (unity_4LightPosZ0 - tmpvar_7.z);
  lengthSq_27 = (tmpvar_28 * tmpvar_28);
  lengthSq_27 = (lengthSq_27 + (tmpvar_29 * tmpvar_29));
  lengthSq_27 = (lengthSq_27 + (tmpvar_30 * tmpvar_30));
  ndotl_26 = (tmpvar_28 * normal_24.x);
  ndotl_26 = (ndotl_26 + (tmpvar_29 * normal_24.y));
  ndotl_26 = (ndotl_26 + (tmpvar_30 * normal_24.z));
  highp vec4 tmpvar_31;
  tmpvar_31 = max (vec4(0.0, 0.0, 0.0, 0.0), (ndotl_26 * inversesqrt(lengthSq_27)));
  ndotl_26 = tmpvar_31;
  highp vec4 tmpvar_32;
  tmpvar_32 = (tmpvar_31 * (1.0/((1.0 + 
    (lengthSq_27 * lightAttenSq_23)
  ))));
  col_25 = (lightColor0_19 * tmpvar_32.x);
  col_25 = (col_25 + (lightColor1_20 * tmpvar_32.y));
  col_25 = (col_25 + (lightColor2_21 * tmpvar_32.z));
  col_25 = (col_25 + (lightColor3_22 * tmpvar_32.w));
  tmpvar_4 = col_25;
  mediump vec3 normal_33;
  normal_33 = worldNormal_3;
  mediump vec3 ambient_34;
  mediump vec4 tmpvar_35;
  tmpvar_35.w = 1.0;
  tmpvar_35.xyz = normal_33;
  mediump vec3 res_36;
  mediump vec3 x_37;
  x_37.x = dot (unity_SHAr, tmpvar_35);
  x_37.y = dot (unity_SHAg, tmpvar_35);
  x_37.z = dot (unity_SHAb, tmpvar_35);
  mediump vec3 x1_38;
  mediump vec4 tmpvar_39;
  tmpvar_39 = (normal_33.xyzz * normal_33.yzzx);
  x1_38.x = dot (unity_SHBr, tmpvar_39);
  x1_38.y = dot (unity_SHBg, tmpvar_39);
  x1_38.z = dot (unity_SHBb, tmpvar_39);
  res_36 = (x_37 + (x1_38 + (unity_SHC.xyz * 
    ((normal_33.x * normal_33.x) - (normal_33.y * normal_33.y))
  )));
  res_36 = max (((1.055 * 
    pow (max (res_36, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  ambient_34 = (tmpvar_4 + max (vec3(0.0, 0.0, 0.0), res_36));
  tmpvar_4 = ambient_34;
  tmpvar_5 = (unity_World2Shadow[0] * tmpvar_7);
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_16;
  xlv_TEXCOORD2 = tmpvar_17;
  xlv_TEXCOORD3 = tmpvar_18;
  xlv_TEXCOORD4 = ambient_34;
  xlv_TEXCOORD5 = tmpvar_5;
}


#endif
#ifdef FRAGMENT
#extension GL_EXT_shadow_samplers : enable
uniform mediump vec4 _WorldSpaceLightPos0;
uniform mediump vec4 _LightShadowData;
uniform lowp vec4 _LightColor0;
uniform lowp sampler2DShadow _ShadowMapTexture;
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD4;
varying mediump vec4 xlv_TEXCOORD5;
void main ()
{
  mediump float tmpvar_1;
  mediump vec3 tmpvar_2;
  mediump vec3 tmpvar_3;
  lowp vec3 worldN_4;
  lowp vec4 c_5;
  lowp vec3 lightDir_6;
  mediump vec3 tmpvar_7;
  tmpvar_7 = _WorldSpaceLightPos0.xyz;
  lightDir_6 = tmpvar_7;
  lowp vec4 tmpvar_8;
  tmpvar_8 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec3 tmpvar_9;
  tmpvar_9 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
  lowp float shadow_10;
  shadow_10 = (_LightShadowData.x + (shadow2DEXT (_ShadowMapTexture, xlv_TEXCOORD5.xyz) * (1.0 - _LightShadowData.x)));
  highp float tmpvar_11;
  tmpvar_11 = dot (xlv_TEXCOORD1.xyz, tmpvar_9);
  worldN_4.x = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = dot (xlv_TEXCOORD2.xyz, tmpvar_9);
  worldN_4.y = tmpvar_12;
  highp float tmpvar_13;
  tmpvar_13 = dot (xlv_TEXCOORD3.xyz, tmpvar_9);
  worldN_4.z = tmpvar_13;
  tmpvar_2 = _LightColor0.xyz;
  tmpvar_3 = lightDir_6;
  tmpvar_1 = shadow_10;
  mediump vec3 tmpvar_14;
  tmpvar_14 = (tmpvar_2 * tmpvar_1);
  tmpvar_2 = tmpvar_14;
  lowp vec4 c_15;
  lowp vec4 c_16;
  lowp float diff_17;
  mediump float tmpvar_18;
  tmpvar_18 = max (0.0, dot (worldN_4, tmpvar_3));
  diff_17 = tmpvar_18;
  c_16.xyz = ((tmpvar_8.xyz * tmpvar_14) * diff_17);
  c_16.w = tmpvar_8.w;
  c_15.w = c_16.w;
  c_15.xyz = (c_16.xyz + (tmpvar_8.xyz * xlv_TEXCOORD4));
  c_5.xyz = c_15.xyz;
  c_5.w = 1.0;
  gl_FragData[0] = c_5;
}


#endif
"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "VERTEXLIGHT_ON" }
"#ifdef VERTEX
#version 300 es
uniform 	vec4 unity_4LightPosX0;
uniform 	vec4 unity_4LightPosY0;
uniform 	vec4 unity_4LightPosZ0;
uniform 	mediump vec4 unity_4LightAtten0;
uniform 	mediump vec4 unity_LightColor[8];
uniform 	mediump vec4 unity_SHAr;
uniform 	mediump vec4 unity_SHAg;
uniform 	mediump vec4 unity_SHAb;
uniform 	mediump vec4 unity_SHBr;
uniform 	mediump vec4 unity_SHBg;
uniform 	mediump vec4 unity_SHBb;
uniform 	mediump vec4 unity_SHC;
uniform 	mat4x4 unity_World2Shadow[4];
uniform 	mat4x4 glstate_matrix_mvp;
uniform 	mat4x4 _Object2World;
uniform 	mat4x4 _World2Object;
uniform 	vec4 unity_WorldTransformParams;
uniform 	vec4 _MainTex_ST;
in highp vec4 in_POSITION0;
in highp vec4 in_TANGENT0;
in highp vec3 in_NORMAL0;
in highp vec4 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
out highp vec4 vs_TEXCOORD1;
out highp vec4 vs_TEXCOORD2;
out highp vec4 vs_TEXCOORD3;
out mediump vec3 vs_TEXCOORD4;
out mediump vec4 vs_TEXCOORD5;
vec4 u_xlat0;
vec4 u_xlat1;
vec4 u_xlat2;
mediump vec4 u_xlat16_2;
vec4 u_xlat3;
lowp vec3 u_xlat10_3;
vec4 u_xlat4;
mediump vec3 u_xlat16_5;
mediump vec3 u_xlat16_6;
vec3 u_xlat7;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat0.x = in_TANGENT0.w * unity_WorldTransformParams.w;
    u_xlat7.xyz = in_TANGENT0.yyy * _Object2World[1].yzx;
    u_xlat7.xyz = _Object2World[0].yzx * in_TANGENT0.xxx + u_xlat7.xyz;
    u_xlat7.xyz = _Object2World[2].yzx * in_TANGENT0.zzz + u_xlat7.xyz;
    u_xlat1.x = dot(u_xlat7.xyz, u_xlat7.xyz);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat7.xyz = u_xlat7.xyz * u_xlat1.xxx;
    u_xlat1.x = in_NORMAL0.x * _World2Object[0].x;
    u_xlat1.y = in_NORMAL0.x * _World2Object[1].x;
    u_xlat1.zw = in_NORMAL0.xx * _World2Object[2].xx;
    u_xlat2.x = in_NORMAL0.y * _World2Object[0].y;
    u_xlat2.y = in_NORMAL0.y * _World2Object[1].y;
    u_xlat2.zw = in_NORMAL0.yy * _World2Object[2].yy;
    u_xlat1 = u_xlat1 + u_xlat2;
    u_xlat2.x = in_NORMAL0.z * _World2Object[0].z;
    u_xlat2.y = in_NORMAL0.z * _World2Object[1].z;
    u_xlat2.zw = in_NORMAL0.zz * _World2Object[2].zz;
    u_xlat1 = u_xlat1 + u_xlat2;
    u_xlat2.x = dot(u_xlat1.xyw, u_xlat1.xyw);
    u_xlat2.x = inversesqrt(u_xlat2.x);
    u_xlat1 = u_xlat1.xywz * u_xlat2.xxxx;
    u_xlat10_3.xyz = u_xlat7.xyz * u_xlat1.zxy;
    u_xlat10_3.xyz = u_xlat1.yzx * u_xlat7.yzx + (-u_xlat10_3.xyz);
    u_xlat10_3.xyz = u_xlat0.xxx * u_xlat10_3.xyz;
    vs_TEXCOORD1.y = u_xlat10_3.x;
    vs_TEXCOORD1.x = u_xlat7.z;
    vs_TEXCOORD1.z = u_xlat1.x;
    u_xlat2.xyz = in_POSITION0.yyy * _Object2World[1].xyz;
    u_xlat2.xyz = _Object2World[0].xyz * in_POSITION0.xxx + u_xlat2.xyz;
    u_xlat2.xyz = _Object2World[2].xyz * in_POSITION0.zzz + u_xlat2.xyz;
    u_xlat2.xyz = _Object2World[3].xyz * in_POSITION0.www + u_xlat2.xyz;
    vs_TEXCOORD1.w = u_xlat2.x;
    vs_TEXCOORD2.x = u_xlat7.x;
    vs_TEXCOORD3.x = u_xlat7.y;
    vs_TEXCOORD2.y = u_xlat10_3.y;
    vs_TEXCOORD3.y = u_xlat10_3.z;
    vs_TEXCOORD2.z = u_xlat1.y;
    vs_TEXCOORD2.w = u_xlat2.y;
    vs_TEXCOORD3.z = u_xlat1.z;
    vs_TEXCOORD3.w = u_xlat2.z;
    u_xlat0 = (-u_xlat2.yyyy) + unity_4LightPosY0;
    u_xlat3 = u_xlat1.yyyy * u_xlat0;
    u_xlat0 = u_xlat0 * u_xlat0;
    u_xlat4 = (-u_xlat2.xxxx) + unity_4LightPosX0;
    u_xlat2 = (-u_xlat2.zzzz) + unity_4LightPosZ0;
    u_xlat3 = u_xlat4 * u_xlat1.xxxx + u_xlat3;
    u_xlat0 = u_xlat4 * u_xlat4 + u_xlat0;
    u_xlat0 = u_xlat2 * u_xlat2 + u_xlat0;
    u_xlat2 = u_xlat2 * u_xlat1.zzwz + u_xlat3;
    u_xlat3 = inversesqrt(u_xlat0);
    u_xlat0 = u_xlat0 * unity_4LightAtten0 + vec4(1.0, 1.0, 1.0, 1.0);
    u_xlat0 = vec4(1.0, 1.0, 1.0, 1.0) / u_xlat0;
    u_xlat2 = u_xlat2 * u_xlat3;
    u_xlat2 = max(u_xlat2, vec4(0.0, 0.0, 0.0, 0.0));
    u_xlat0 = u_xlat0 * u_xlat2;
    u_xlat2.xyz = u_xlat0.yyy * unity_LightColor[1].xyz;
    u_xlat2.xyz = unity_LightColor[0].xyz * u_xlat0.xxx + u_xlat2.xyz;
    u_xlat0.xyz = unity_LightColor[2].xyz * u_xlat0.zzz + u_xlat2.xyz;
    u_xlat0.xyz = unity_LightColor[3].xyz * u_xlat0.www + u_xlat0.xyz;
    u_xlat16_5.x = u_xlat1.y * u_xlat1.y;
    u_xlat16_5.x = u_xlat1.x * u_xlat1.x + (-u_xlat16_5.x);
    u_xlat16_2 = u_xlat1.yzwx * u_xlat1.xywz;
    u_xlat16_6.x = dot(unity_SHBr, u_xlat16_2);
    u_xlat16_6.y = dot(unity_SHBg, u_xlat16_2);
    u_xlat16_6.z = dot(unity_SHBb, u_xlat16_2);
    u_xlat16_5.xyz = unity_SHC.xyz * u_xlat16_5.xxx + u_xlat16_6.xyz;
    u_xlat1.w = 1.0;
    u_xlat16_6.x = dot(unity_SHAr, u_xlat1);
    u_xlat16_6.y = dot(unity_SHAg, u_xlat1);
    u_xlat16_6.z = dot(unity_SHAb, u_xlat1);
    u_xlat16_5.xyz = u_xlat16_5.xyz + u_xlat16_6.xyz;
    u_xlat16_5.xyz = max(u_xlat16_5.xyz, vec3(0.0, 0.0, 0.0));
    u_xlat1.xyz = log2(u_xlat16_5.xyz);
    u_xlat1.xyz = u_xlat1.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
    u_xlat1.xyz = exp2(u_xlat1.xyz);
    u_xlat1.xyz = u_xlat1.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
    u_xlat1.xyz = max(u_xlat1.xyz, vec3(0.0, 0.0, 0.0));
    vs_TEXCOORD4.xyz = u_xlat0.xyz + u_xlat1.xyz;
    u_xlat0 = in_POSITION0.yyyy * _Object2World[1];
    u_xlat0 = _Object2World[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = _Object2World[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat0 = _Object2World[3] * in_POSITION0.wwww + u_xlat0;
    u_xlat1 = u_xlat0.yyyy * unity_World2Shadow[0][1];
    u_xlat1 = unity_World2Shadow[0][0] * u_xlat0.xxxx + u_xlat1;
    u_xlat1 = unity_World2Shadow[0][2] * u_xlat0.zzzz + u_xlat1;
    u_xlat0 = unity_World2Shadow[0][3] * u_xlat0.wwww + u_xlat1;
    vs_TEXCOORD5 = u_xlat0;
    return;
}
#endif
#ifdef FRAGMENT
#version 300 es
precision highp int;
uniform 	mediump vec4 _WorldSpaceLightPos0;
uniform 	mediump vec4 _LightShadowData;
uniform 	lowp vec4 _LightColor0;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _BumpMap;
uniform lowp sampler2DShadow hlslcc_zcmp_ShadowMapTexture;
uniform lowp sampler2D _ShadowMapTexture;
in highp vec2 vs_TEXCOORD0;
in highp vec4 vs_TEXCOORD1;
in highp vec4 vs_TEXCOORD2;
in highp vec4 vs_TEXCOORD3;
in mediump vec3 vs_TEXCOORD4;
in mediump vec4 vs_TEXCOORD5;
layout(location = 0) out lowp vec4 SV_Target0;
vec3 u_xlat0;
lowp vec3 u_xlat10_0;
lowp vec3 u_xlat10_1;
mediump vec3 u_xlat16_2;
mediump vec3 u_xlat16_3;
mediump vec3 u_xlat16_6;
mediump float u_xlat16_10;
void main()
{
    u_xlat10_0.xyz = texture(_BumpMap, vs_TEXCOORD0.xy).xyz;
    u_xlat10_1.xyz = u_xlat10_0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
    u_xlat0.x = dot(vs_TEXCOORD1.xyz, u_xlat10_1.xyz);
    u_xlat0.y = dot(vs_TEXCOORD2.xyz, u_xlat10_1.xyz);
    u_xlat0.z = dot(vs_TEXCOORD3.xyz, u_xlat10_1.xyz);
    u_xlat16_2.x = dot(u_xlat0.xyz, _WorldSpaceLightPos0.xyz);
    u_xlat16_2.x = max(u_xlat16_2.x, 0.0);
    vec3 txVec1 = vec3(vs_TEXCOORD5.xy,vs_TEXCOORD5.z);
    u_xlat16_6.x = textureLod(hlslcc_zcmp_ShadowMapTexture, txVec1, 0.0);
    u_xlat16_10 = (-_LightShadowData.x) + 1.0;
    u_xlat16_6.x = u_xlat16_6.x * u_xlat16_10 + _LightShadowData.x;
    u_xlat16_6.xyz = u_xlat16_6.xxx * _LightColor0.xyz;
    u_xlat10_0.xyz = texture(_MainTex, vs_TEXCOORD0.xy).xyz;
    u_xlat16_6.xyz = u_xlat16_6.xyz * u_xlat10_0.xyz;
    u_xlat16_3.xyz = u_xlat10_0.xyz * vs_TEXCOORD4.xyz;
    u_xlat16_2.xyz = u_xlat16_6.xyz * u_xlat16_2.xxx + u_xlat16_3.xyz;
    SV_Target0.xyz = u_xlat16_2.xyz;
    SV_Target0.w = 1.0;
    return;
}
#endif
"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"// shader disassembly not supported on gles"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_OFF" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"// shader disassembly not supported on gles3"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"// shader disassembly not supported on gles"
}
SubProgram "gles " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"// shader disassembly not supported on gles"
}
SubProgram "gles3 " {
Keywords { "DIRECTIONAL" "SHADOWS_SCREEN" "SHADOWS_NATIVE" "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" }
"// shader disassembly not supported on gles3"
}
}
 }


 // Stats for Vertex shader:
 //        gles : 8 math, 1 texture
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassBase" "RenderType"="Opaque" }
  GpuProgramID 77865
Program "vp" {
SubProgram "gles " {
// Stats: 8 math, 1 textures
"#version 100

#ifdef VERTEX
attribute vec4 _glesTANGENT;
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_WorldTransformParams;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
void main ()
{
  lowp float tangentSign_1;
  lowp vec3 worldTangent_2;
  lowp vec3 worldNormal_3;
  highp vec3 tmpvar_4;
  tmpvar_4 = (_Object2World * _glesVertex).xyz;
  highp vec4 v_5;
  v_5.x = _World2Object[0].x;
  v_5.y = _World2Object[1].x;
  v_5.z = _World2Object[2].x;
  v_5.w = _World2Object[3].x;
  highp vec4 v_6;
  v_6.x = _World2Object[0].y;
  v_6.y = _World2Object[1].y;
  v_6.z = _World2Object[2].y;
  v_6.w = _World2Object[3].y;
  highp vec4 v_7;
  v_7.x = _World2Object[0].z;
  v_7.y = _World2Object[1].z;
  v_7.z = _World2Object[2].z;
  v_7.w = _World2Object[3].z;
  highp vec3 tmpvar_8;
  tmpvar_8 = normalize(((
    (v_5.xyz * _glesNormal.x)
   + 
    (v_6.xyz * _glesNormal.y)
  ) + (v_7.xyz * _glesNormal.z)));
  worldNormal_3 = tmpvar_8;
  highp mat3 tmpvar_9;
  tmpvar_9[0] = _Object2World[0].xyz;
  tmpvar_9[1] = _Object2World[1].xyz;
  tmpvar_9[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_10;
  tmpvar_10 = normalize((tmpvar_9 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_10;
  highp float tmpvar_11;
  tmpvar_11 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_11;
  lowp vec3 tmpvar_12;
  tmpvar_12 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_13;
  tmpvar_13.x = worldTangent_2.x;
  tmpvar_13.y = tmpvar_12.x;
  tmpvar_13.z = worldNormal_3.x;
  tmpvar_13.w = tmpvar_4.x;
  highp vec4 tmpvar_14;
  tmpvar_14.x = worldTangent_2.y;
  tmpvar_14.y = tmpvar_12.y;
  tmpvar_14.z = worldNormal_3.y;
  tmpvar_14.w = tmpvar_4.y;
  highp vec4 tmpvar_15;
  tmpvar_15.x = worldTangent_2.z;
  tmpvar_15.y = tmpvar_12.z;
  tmpvar_15.z = worldNormal_3.z;
  tmpvar_15.w = tmpvar_4.z;
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_13;
  xlv_TEXCOORD2 = tmpvar_14;
  xlv_TEXCOORD3 = tmpvar_15;
}


#endif
#ifdef FRAGMENT
uniform sampler2D _BumpMap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
void main ()
{
  lowp vec4 res_1;
  lowp vec3 worldN_2;
  lowp vec3 tmpvar_3;
  tmpvar_3 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
  highp float tmpvar_4;
  tmpvar_4 = dot (xlv_TEXCOORD1.xyz, tmpvar_3);
  worldN_2.x = tmpvar_4;
  highp float tmpvar_5;
  tmpvar_5 = dot (xlv_TEXCOORD2.xyz, tmpvar_3);
  worldN_2.y = tmpvar_5;
  highp float tmpvar_6;
  tmpvar_6 = dot (xlv_TEXCOORD3.xyz, tmpvar_3);
  worldN_2.z = tmpvar_6;
  res_1.xyz = ((worldN_2 * 0.5) + 0.5);
  res_1.w = 0.0;
  gl_FragData[0] = res_1;
}


#endif
"
}
SubProgram "gles3 " {
"#ifdef VERTEX
#version 300 es
uniform 	mat4x4 glstate_matrix_mvp;
uniform 	mat4x4 _Object2World;
uniform 	mat4x4 _World2Object;
uniform 	vec4 unity_WorldTransformParams;
uniform 	vec4 _MainTex_ST;
in highp vec4 in_POSITION0;
in highp vec4 in_TANGENT0;
in highp vec3 in_NORMAL0;
in highp vec4 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
out highp vec4 vs_TEXCOORD1;
out highp vec4 vs_TEXCOORD2;
out highp vec4 vs_TEXCOORD3;
vec4 u_xlat0;
vec3 u_xlat1;
lowp vec3 u_xlat10_2;
vec3 u_xlat3;
float u_xlat4;
float u_xlat12;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat0.y = in_NORMAL0.x * _World2Object[0].x;
    u_xlat0.z = in_NORMAL0.x * _World2Object[1].x;
    u_xlat0.x = in_NORMAL0.x * _World2Object[2].x;
    u_xlat1.y = in_NORMAL0.y * _World2Object[0].y;
    u_xlat1.z = in_NORMAL0.y * _World2Object[1].y;
    u_xlat1.x = in_NORMAL0.y * _World2Object[2].y;
    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
    u_xlat1.y = in_NORMAL0.z * _World2Object[0].z;
    u_xlat1.z = in_NORMAL0.z * _World2Object[1].z;
    u_xlat1.x = in_NORMAL0.z * _World2Object[2].z;
    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat12 = inversesqrt(u_xlat12);
    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
    vs_TEXCOORD1.z = u_xlat0.y;
    u_xlat1.xyz = in_TANGENT0.yyy * _Object2World[1].yzx;
    u_xlat1.xyz = _Object2World[0].yzx * in_TANGENT0.xxx + u_xlat1.xyz;
    u_xlat1.xyz = _Object2World[2].yzx * in_TANGENT0.zzz + u_xlat1.xyz;
    u_xlat12 = dot(u_xlat1.xyz, u_xlat1.xyz);
    u_xlat12 = inversesqrt(u_xlat12);
    u_xlat1.xyz = vec3(u_xlat12) * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.xyz * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.zxy * u_xlat1.yzx + (-u_xlat10_2.xyz);
    u_xlat4 = in_TANGENT0.w * unity_WorldTransformParams.w;
    u_xlat10_2.xyz = vec3(u_xlat4) * u_xlat10_2.xyz;
    vs_TEXCOORD1.y = u_xlat10_2.x;
    u_xlat3.xyz = in_POSITION0.yyy * _Object2World[1].xyz;
    u_xlat3.xyz = _Object2World[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[3].xyz * in_POSITION0.www + u_xlat3.xyz;
    vs_TEXCOORD1.w = u_xlat3.x;
    vs_TEXCOORD1.x = u_xlat1.z;
    vs_TEXCOORD2.z = u_xlat0.z;
    vs_TEXCOORD3.z = u_xlat0.x;
    vs_TEXCOORD2.x = u_xlat1.x;
    vs_TEXCOORD3.x = u_xlat1.y;
    vs_TEXCOORD2.w = u_xlat3.y;
    vs_TEXCOORD3.w = u_xlat3.z;
    vs_TEXCOORD2.y = u_xlat10_2.y;
    vs_TEXCOORD3.y = u_xlat10_2.z;
    return;
}
#endif
#ifdef FRAGMENT
#version 300 es
precision highp int;
uniform lowp sampler2D _BumpMap;
in highp vec2 vs_TEXCOORD0;
in highp vec4 vs_TEXCOORD1;
in highp vec4 vs_TEXCOORD2;
in highp vec4 vs_TEXCOORD3;
layout(location = 0) out lowp vec4 SV_Target0;
vec3 u_xlat0;
lowp vec3 u_xlat10_0;
lowp vec3 u_xlat10_1;
void main()
{
    u_xlat10_0.xyz = texture(_BumpMap, vs_TEXCOORD0.xy).xyz;
    u_xlat10_1.xyz = u_xlat10_0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
    u_xlat0.x = dot(vs_TEXCOORD1.xyz, u_xlat10_1.xyz);
    u_xlat0.y = dot(vs_TEXCOORD2.xyz, u_xlat10_1.xyz);
    u_xlat0.z = dot(vs_TEXCOORD3.xyz, u_xlat10_1.xyz);
    SV_Target0.xyz = u_xlat0.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.5, 0.5, 0.5);
    SV_Target0.w = 0.0;
    return;
}
#endif
"
}
}
Program "fp" {
SubProgram "gles " {
"// shader disassembly not supported on gles"
}
SubProgram "gles3 " {
"// shader disassembly not supported on gles3"
}
}
 }


 // Stats for Vertex shader:
 //        gles : 5 avg math (4..6), 2 texture
 Pass {
  Name "PREPASS"
  Tags { "LIGHTMODE"="PrePassFinal" "RenderType"="Opaque" }
  ZWrite Off
  GpuProgramID 147283
Program "vp" {
SubProgram "gles " {
// Stats: 6 math, 2 textures
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"#version 100

#ifdef VERTEX
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _ProjectionParams;
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD4;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_1 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 o_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (tmpvar_1 * 0.5);
  highp vec2 tmpvar_6;
  tmpvar_6.x = tmpvar_5.x;
  tmpvar_6.y = (tmpvar_5.y * _ProjectionParams.x);
  o_4.xy = (tmpvar_6 + tmpvar_5.w);
  o_4.zw = tmpvar_1.zw;
  tmpvar_2.zw = vec2(0.0, 0.0);
  tmpvar_2.xy = vec2(0.0, 0.0);
  highp vec4 v_7;
  v_7.x = _World2Object[0].x;
  v_7.y = _World2Object[1].x;
  v_7.z = _World2Object[2].x;
  v_7.w = _World2Object[3].x;
  highp vec4 v_8;
  v_8.x = _World2Object[0].y;
  v_8.y = _World2Object[1].y;
  v_8.z = _World2Object[2].y;
  v_8.w = _World2Object[3].y;
  highp vec4 v_9;
  v_9.x = _World2Object[0].z;
  v_9.y = _World2Object[1].z;
  v_9.z = _World2Object[2].z;
  v_9.w = _World2Object[3].z;
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = normalize(((
    (v_7.xyz * _glesNormal.x)
   + 
    (v_8.xyz * _glesNormal.y)
  ) + (v_9.xyz * _glesNormal.z)));
  mediump vec4 normal_11;
  normal_11 = tmpvar_10;
  mediump vec3 res_12;
  mediump vec3 x_13;
  x_13.x = dot (unity_SHAr, normal_11);
  x_13.y = dot (unity_SHAg, normal_11);
  x_13.z = dot (unity_SHAb, normal_11);
  mediump vec3 x1_14;
  mediump vec4 tmpvar_15;
  tmpvar_15 = (normal_11.xyzz * normal_11.yzzx);
  x1_14.x = dot (unity_SHBr, tmpvar_15);
  x1_14.y = dot (unity_SHBg, tmpvar_15);
  x1_14.z = dot (unity_SHBb, tmpvar_15);
  res_12 = (x_13 + (x1_14 + (unity_SHC.xyz * 
    ((normal_11.x * normal_11.x) - (normal_11.y * normal_11.y))
  )));
  res_12 = max (((1.055 * 
    pow (max (res_12, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  tmpvar_3 = res_12;
  gl_Position = tmpvar_1;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = (_Object2World * _glesVertex).xyz;
  xlv_TEXCOORD2 = o_4;
  xlv_TEXCOORD3 = tmpvar_2;
  xlv_TEXCOORD4 = tmpvar_3;
}


#endif
#ifdef FRAGMENT
uniform sampler2D _MainTex;
uniform sampler2D _LightBuffer;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
  light_3 = tmpvar_5;
  light_3 = -(log2(max (light_3, vec4(0.001, 0.001, 0.001, 0.001))));
  light_3.xyz = (light_3.xyz + xlv_TEXCOORD4);
  lowp vec4 c_6;
  c_6.xyz = (tmpvar_4.xyz * light_3.xyz);
  c_6.w = tmpvar_4.w;
  c_2.xyz = c_6.xyz;
  c_2.w = 1.0;
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}


#endif
"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"#ifdef VERTEX
#version 300 es
uniform 	vec4 _ProjectionParams;
uniform 	mediump vec4 unity_SHAr;
uniform 	mediump vec4 unity_SHAg;
uniform 	mediump vec4 unity_SHAb;
uniform 	mediump vec4 unity_SHBr;
uniform 	mediump vec4 unity_SHBg;
uniform 	mediump vec4 unity_SHBb;
uniform 	mediump vec4 unity_SHC;
uniform 	mat4x4 glstate_matrix_mvp;
uniform 	mat4x4 _Object2World;
uniform 	mat4x4 _World2Object;
uniform 	vec4 _MainTex_ST;
in highp vec4 in_POSITION0;
in highp vec3 in_NORMAL0;
in highp vec4 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
out highp vec3 vs_TEXCOORD1;
out highp vec4 vs_TEXCOORD2;
out highp vec4 vs_TEXCOORD3;
out highp vec3 vs_TEXCOORD4;
vec4 u_xlat0;
vec4 u_xlat1;
mediump vec4 u_xlat16_1;
mediump vec3 u_xlat16_2;
mediump vec3 u_xlat16_3;
float u_xlat12;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
    gl_Position = u_xlat0;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat1.xyz = in_POSITION0.yyy * _Object2World[1].xyz;
    u_xlat1.xyz = _Object2World[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
    u_xlat1.xyz = _Object2World[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
    vs_TEXCOORD1.xyz = _Object2World[3].xyz * in_POSITION0.www + u_xlat1.xyz;
    u_xlat0.y = u_xlat0.y * _ProjectionParams.x;
    u_xlat1.xzw = u_xlat0.xwy * vec3(0.5, 0.5, 0.5);
    vs_TEXCOORD2.zw = u_xlat0.zw;
    vs_TEXCOORD2.xy = u_xlat1.zz + u_xlat1.xw;
    vs_TEXCOORD3 = vec4(0.0, 0.0, 0.0, 0.0);
    u_xlat0.x = in_NORMAL0.x * _World2Object[0].x;
    u_xlat0.y = in_NORMAL0.x * _World2Object[1].x;
    u_xlat0.z = in_NORMAL0.x * _World2Object[2].x;
    u_xlat1.x = in_NORMAL0.y * _World2Object[0].y;
    u_xlat1.y = in_NORMAL0.y * _World2Object[1].y;
    u_xlat1.z = in_NORMAL0.y * _World2Object[2].y;
    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
    u_xlat1.x = in_NORMAL0.z * _World2Object[0].z;
    u_xlat1.y = in_NORMAL0.z * _World2Object[1].z;
    u_xlat1.z = in_NORMAL0.z * _World2Object[2].z;
    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat12 = inversesqrt(u_xlat12);
    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
    u_xlat16_2.x = u_xlat0.y * u_xlat0.y;
    u_xlat16_2.x = u_xlat0.x * u_xlat0.x + (-u_xlat16_2.x);
    u_xlat16_1 = u_xlat0.yzzx * u_xlat0.xyzz;
    u_xlat16_3.x = dot(unity_SHBr, u_xlat16_1);
    u_xlat16_3.y = dot(unity_SHBg, u_xlat16_1);
    u_xlat16_3.z = dot(unity_SHBb, u_xlat16_1);
    u_xlat16_2.xyz = unity_SHC.xyz * u_xlat16_2.xxx + u_xlat16_3.xyz;
    u_xlat0.w = 1.0;
    u_xlat16_3.x = dot(unity_SHAr, u_xlat0);
    u_xlat16_3.y = dot(unity_SHAg, u_xlat0);
    u_xlat16_3.z = dot(unity_SHAb, u_xlat0);
    u_xlat16_2.xyz = u_xlat16_2.xyz + u_xlat16_3.xyz;
    u_xlat16_2.xyz = max(u_xlat16_2.xyz, vec3(0.0, 0.0, 0.0));
    u_xlat0.xyz = log2(u_xlat16_2.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
    u_xlat0.xyz = exp2(u_xlat0.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
    vs_TEXCOORD4.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
    return;
}
#endif
#ifdef FRAGMENT
#version 300 es
precision highp int;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _LightBuffer;
in highp vec2 vs_TEXCOORD0;
in highp vec4 vs_TEXCOORD2;
in highp vec3 vs_TEXCOORD4;
layout(location = 0) out lowp vec4 SV_Target0;
vec3 u_xlat0;
lowp vec3 u_xlat10_0;
mediump vec3 u_xlat16_1;
lowp vec3 u_xlat10_2;
void main()
{
    u_xlat0.xy = vs_TEXCOORD2.xy / vs_TEXCOORD2.ww;
    u_xlat10_0.xyz = texture(_LightBuffer, u_xlat0.xy).xyz;
    u_xlat16_1.xyz = max(u_xlat10_0.xyz, vec3(0.00100000005, 0.00100000005, 0.00100000005));
    u_xlat16_1.xyz = log2(u_xlat16_1.xyz);
    u_xlat0.xyz = (-u_xlat16_1.xyz) + vs_TEXCOORD4.xyz;
    u_xlat10_2.xyz = texture(_MainTex, vs_TEXCOORD0.xy).xyz;
    u_xlat16_1.xyz = u_xlat0.xyz * u_xlat10_2.xyz;
    SV_Target0.xyz = u_xlat16_1.xyz;
    SV_Target0.w = 1.0;
    return;
}
#endif
"
}
SubProgram "gles " {
// Stats: 4 math, 2 textures
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
"#version 100

#ifdef VERTEX
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform highp vec4 _ProjectionParams;
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec3 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec3 xlv_TEXCOORD4;
void main ()
{
  highp vec4 tmpvar_1;
  highp vec4 tmpvar_2;
  highp vec3 tmpvar_3;
  tmpvar_1 = (glstate_matrix_mvp * _glesVertex);
  highp vec4 o_4;
  highp vec4 tmpvar_5;
  tmpvar_5 = (tmpvar_1 * 0.5);
  highp vec2 tmpvar_6;
  tmpvar_6.x = tmpvar_5.x;
  tmpvar_6.y = (tmpvar_5.y * _ProjectionParams.x);
  o_4.xy = (tmpvar_6 + tmpvar_5.w);
  o_4.zw = tmpvar_1.zw;
  tmpvar_2.zw = vec2(0.0, 0.0);
  tmpvar_2.xy = vec2(0.0, 0.0);
  highp vec4 v_7;
  v_7.x = _World2Object[0].x;
  v_7.y = _World2Object[1].x;
  v_7.z = _World2Object[2].x;
  v_7.w = _World2Object[3].x;
  highp vec4 v_8;
  v_8.x = _World2Object[0].y;
  v_8.y = _World2Object[1].y;
  v_8.z = _World2Object[2].y;
  v_8.w = _World2Object[3].y;
  highp vec4 v_9;
  v_9.x = _World2Object[0].z;
  v_9.y = _World2Object[1].z;
  v_9.z = _World2Object[2].z;
  v_9.w = _World2Object[3].z;
  highp vec4 tmpvar_10;
  tmpvar_10.w = 1.0;
  tmpvar_10.xyz = normalize(((
    (v_7.xyz * _glesNormal.x)
   + 
    (v_8.xyz * _glesNormal.y)
  ) + (v_9.xyz * _glesNormal.z)));
  mediump vec4 normal_11;
  normal_11 = tmpvar_10;
  mediump vec3 res_12;
  mediump vec3 x_13;
  x_13.x = dot (unity_SHAr, normal_11);
  x_13.y = dot (unity_SHAg, normal_11);
  x_13.z = dot (unity_SHAb, normal_11);
  mediump vec3 x1_14;
  mediump vec4 tmpvar_15;
  tmpvar_15 = (normal_11.xyzz * normal_11.yzzx);
  x1_14.x = dot (unity_SHBr, tmpvar_15);
  x1_14.y = dot (unity_SHBg, tmpvar_15);
  x1_14.z = dot (unity_SHBb, tmpvar_15);
  res_12 = (x_13 + (x1_14 + (unity_SHC.xyz * 
    ((normal_11.x * normal_11.x) - (normal_11.y * normal_11.y))
  )));
  res_12 = max (((1.055 * 
    pow (max (res_12, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  tmpvar_3 = res_12;
  gl_Position = tmpvar_1;
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = (_Object2World * _glesVertex).xyz;
  xlv_TEXCOORD2 = o_4;
  xlv_TEXCOORD3 = tmpvar_2;
  xlv_TEXCOORD4 = tmpvar_3;
}


#endif
#ifdef FRAGMENT
uniform sampler2D _MainTex;
uniform sampler2D _LightBuffer;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec3 xlv_TEXCOORD4;
void main ()
{
  lowp vec4 tmpvar_1;
  mediump vec4 c_2;
  mediump vec4 light_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec4 tmpvar_5;
  tmpvar_5 = texture2DProj (_LightBuffer, xlv_TEXCOORD2);
  light_3 = tmpvar_5;
  mediump vec4 tmpvar_6;
  tmpvar_6 = max (light_3, vec4(0.001, 0.001, 0.001, 0.001));
  light_3.w = tmpvar_6.w;
  light_3.xyz = (tmpvar_6.xyz + xlv_TEXCOORD4);
  lowp vec4 c_7;
  c_7.xyz = (tmpvar_4.xyz * light_3.xyz);
  c_7.w = tmpvar_4.w;
  c_2.xyz = c_7.xyz;
  c_2.w = 1.0;
  tmpvar_1 = c_2;
  gl_FragData[0] = tmpvar_1;
}


#endif
"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
"#ifdef VERTEX
#version 300 es
uniform 	vec4 _ProjectionParams;
uniform 	mediump vec4 unity_SHAr;
uniform 	mediump vec4 unity_SHAg;
uniform 	mediump vec4 unity_SHAb;
uniform 	mediump vec4 unity_SHBr;
uniform 	mediump vec4 unity_SHBg;
uniform 	mediump vec4 unity_SHBb;
uniform 	mediump vec4 unity_SHC;
uniform 	mat4x4 glstate_matrix_mvp;
uniform 	mat4x4 _Object2World;
uniform 	mat4x4 _World2Object;
uniform 	vec4 _MainTex_ST;
in highp vec4 in_POSITION0;
in highp vec3 in_NORMAL0;
in highp vec4 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
out highp vec3 vs_TEXCOORD1;
out highp vec4 vs_TEXCOORD2;
out highp vec4 vs_TEXCOORD3;
out highp vec3 vs_TEXCOORD4;
vec4 u_xlat0;
vec4 u_xlat1;
mediump vec4 u_xlat16_1;
mediump vec3 u_xlat16_2;
mediump vec3 u_xlat16_3;
float u_xlat12;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
    gl_Position = u_xlat0;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat1.xyz = in_POSITION0.yyy * _Object2World[1].xyz;
    u_xlat1.xyz = _Object2World[0].xyz * in_POSITION0.xxx + u_xlat1.xyz;
    u_xlat1.xyz = _Object2World[2].xyz * in_POSITION0.zzz + u_xlat1.xyz;
    vs_TEXCOORD1.xyz = _Object2World[3].xyz * in_POSITION0.www + u_xlat1.xyz;
    u_xlat0.y = u_xlat0.y * _ProjectionParams.x;
    u_xlat1.xzw = u_xlat0.xwy * vec3(0.5, 0.5, 0.5);
    vs_TEXCOORD2.zw = u_xlat0.zw;
    vs_TEXCOORD2.xy = u_xlat1.zz + u_xlat1.xw;
    vs_TEXCOORD3 = vec4(0.0, 0.0, 0.0, 0.0);
    u_xlat0.x = in_NORMAL0.x * _World2Object[0].x;
    u_xlat0.y = in_NORMAL0.x * _World2Object[1].x;
    u_xlat0.z = in_NORMAL0.x * _World2Object[2].x;
    u_xlat1.x = in_NORMAL0.y * _World2Object[0].y;
    u_xlat1.y = in_NORMAL0.y * _World2Object[1].y;
    u_xlat1.z = in_NORMAL0.y * _World2Object[2].y;
    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
    u_xlat1.x = in_NORMAL0.z * _World2Object[0].z;
    u_xlat1.y = in_NORMAL0.z * _World2Object[1].z;
    u_xlat1.z = in_NORMAL0.z * _World2Object[2].z;
    u_xlat0.xyz = u_xlat0.xyz + u_xlat1.xyz;
    u_xlat12 = dot(u_xlat0.xyz, u_xlat0.xyz);
    u_xlat12 = inversesqrt(u_xlat12);
    u_xlat0.xyz = vec3(u_xlat12) * u_xlat0.xyz;
    u_xlat16_2.x = u_xlat0.y * u_xlat0.y;
    u_xlat16_2.x = u_xlat0.x * u_xlat0.x + (-u_xlat16_2.x);
    u_xlat16_1 = u_xlat0.yzzx * u_xlat0.xyzz;
    u_xlat16_3.x = dot(unity_SHBr, u_xlat16_1);
    u_xlat16_3.y = dot(unity_SHBg, u_xlat16_1);
    u_xlat16_3.z = dot(unity_SHBb, u_xlat16_1);
    u_xlat16_2.xyz = unity_SHC.xyz * u_xlat16_2.xxx + u_xlat16_3.xyz;
    u_xlat0.w = 1.0;
    u_xlat16_3.x = dot(unity_SHAr, u_xlat0);
    u_xlat16_3.y = dot(unity_SHAg, u_xlat0);
    u_xlat16_3.z = dot(unity_SHAb, u_xlat0);
    u_xlat16_2.xyz = u_xlat16_2.xyz + u_xlat16_3.xyz;
    u_xlat16_2.xyz = max(u_xlat16_2.xyz, vec3(0.0, 0.0, 0.0));
    u_xlat0.xyz = log2(u_xlat16_2.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
    u_xlat0.xyz = exp2(u_xlat0.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
    vs_TEXCOORD4.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
    return;
}
#endif
#ifdef FRAGMENT
#version 300 es
precision highp int;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _LightBuffer;
in highp vec2 vs_TEXCOORD0;
in highp vec4 vs_TEXCOORD2;
in highp vec3 vs_TEXCOORD4;
layout(location = 0) out lowp vec4 SV_Target0;
vec3 u_xlat0;
lowp vec3 u_xlat10_0;
mediump vec3 u_xlat16_1;
lowp vec3 u_xlat10_2;
void main()
{
    u_xlat0.xy = vs_TEXCOORD2.xy / vs_TEXCOORD2.ww;
    u_xlat10_0.xyz = texture(_LightBuffer, u_xlat0.xy).xyz;
    u_xlat16_1.xyz = max(u_xlat10_0.xyz, vec3(0.00100000005, 0.00100000005, 0.00100000005));
    u_xlat0.xyz = u_xlat16_1.xyz + vs_TEXCOORD4.xyz;
    u_xlat10_2.xyz = texture(_MainTex, vs_TEXCOORD0.xy).xyz;
    u_xlat16_1.xyz = u_xlat0.xyz * u_xlat10_2.xyz;
    SV_Target0.xyz = u_xlat16_1.xyz;
    SV_Target0.w = 1.0;
    return;
}
#endif
"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"// shader disassembly not supported on gles"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"// shader disassembly not supported on gles3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
"// shader disassembly not supported on gles"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
"// shader disassembly not supported on gles3"
}
}
 }


 // Stats for Vertex shader:
 //        gles : 16 avg math (15..17), 2 texture
 Pass {
  Name "DEFERRED"
  Tags { "LIGHTMODE"="Deferred" "RenderType"="Opaque" }
  GpuProgramID 233564
Program "vp" {
SubProgram "gles " {
// Stats: 17 math, 2 textures
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"#version 100

#ifdef VERTEX
attribute vec4 _glesTANGENT;
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_WorldTransformParams;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD5;
void main ()
{
  lowp float tangentSign_1;
  lowp vec3 worldTangent_2;
  lowp vec3 worldNormal_3;
  highp vec4 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_Object2World * _glesVertex).xyz;
  highp vec4 v_6;
  v_6.x = _World2Object[0].x;
  v_6.y = _World2Object[1].x;
  v_6.z = _World2Object[2].x;
  v_6.w = _World2Object[3].x;
  highp vec4 v_7;
  v_7.x = _World2Object[0].y;
  v_7.y = _World2Object[1].y;
  v_7.z = _World2Object[2].y;
  v_7.w = _World2Object[3].y;
  highp vec4 v_8;
  v_8.x = _World2Object[0].z;
  v_8.y = _World2Object[1].z;
  v_8.z = _World2Object[2].z;
  v_8.w = _World2Object[3].z;
  highp vec3 tmpvar_9;
  tmpvar_9 = normalize(((
    (v_6.xyz * _glesNormal.x)
   + 
    (v_7.xyz * _glesNormal.y)
  ) + (v_8.xyz * _glesNormal.z)));
  worldNormal_3 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_12;
  lowp vec3 tmpvar_13;
  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_14;
  tmpvar_14.x = worldTangent_2.x;
  tmpvar_14.y = tmpvar_13.x;
  tmpvar_14.z = worldNormal_3.x;
  tmpvar_14.w = tmpvar_5.x;
  highp vec4 tmpvar_15;
  tmpvar_15.x = worldTangent_2.y;
  tmpvar_15.y = tmpvar_13.y;
  tmpvar_15.z = worldNormal_3.y;
  tmpvar_15.w = tmpvar_5.y;
  highp vec4 tmpvar_16;
  tmpvar_16.x = worldTangent_2.z;
  tmpvar_16.y = tmpvar_13.z;
  tmpvar_16.z = worldNormal_3.z;
  tmpvar_16.w = tmpvar_5.z;
  tmpvar_4.zw = vec2(0.0, 0.0);
  tmpvar_4.xy = vec2(0.0, 0.0);
  mediump vec3 normal_17;
  normal_17 = worldNormal_3;
  mediump vec4 tmpvar_18;
  tmpvar_18.w = 1.0;
  tmpvar_18.xyz = normal_17;
  mediump vec3 res_19;
  mediump vec3 x_20;
  x_20.x = dot (unity_SHAr, tmpvar_18);
  x_20.y = dot (unity_SHAg, tmpvar_18);
  x_20.z = dot (unity_SHAb, tmpvar_18);
  mediump vec3 x1_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_17.xyzz * normal_17.yzzx);
  x1_21.x = dot (unity_SHBr, tmpvar_22);
  x1_21.y = dot (unity_SHBg, tmpvar_22);
  x1_21.z = dot (unity_SHBb, tmpvar_22);
  res_19 = (x_20 + (x1_21 + (unity_SHC.xyz * 
    ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y))
  )));
  res_19 = max (((1.055 * 
    pow (max (res_19, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_14;
  xlv_TEXCOORD2 = tmpvar_15;
  xlv_TEXCOORD3 = tmpvar_16;
  xlv_TEXCOORD4 = tmpvar_4;
  xlv_TEXCOORD5 = max (vec3(0.0, 0.0, 0.0), res_19);
}


#endif
#ifdef FRAGMENT
#extension GL_EXT_draw_buffers : enable
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD5;
void main ()
{
  mediump vec4 outDiffuse_1;
  mediump vec4 outEmission_2;
  lowp vec3 worldN_3;
  lowp vec4 tmpvar_4;
  tmpvar_4 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec3 tmpvar_5;
  tmpvar_5 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
  highp float tmpvar_6;
  tmpvar_6 = dot (xlv_TEXCOORD1.xyz, tmpvar_5);
  worldN_3.x = tmpvar_6;
  highp float tmpvar_7;
  tmpvar_7 = dot (xlv_TEXCOORD2.xyz, tmpvar_5);
  worldN_3.y = tmpvar_7;
  highp float tmpvar_8;
  tmpvar_8 = dot (xlv_TEXCOORD3.xyz, tmpvar_5);
  worldN_3.z = tmpvar_8;
  mediump vec4 outDiffuseOcclusion_9;
  mediump vec4 outNormal_10;
  mediump vec4 emission_11;
  lowp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = tmpvar_4.xyz;
  outDiffuseOcclusion_9 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13.w = 1.0;
  tmpvar_13.xyz = ((worldN_3 * 0.5) + 0.5);
  outNormal_10 = tmpvar_13;
  lowp vec4 tmpvar_14;
  tmpvar_14.w = 1.0;
  tmpvar_14.xyz = vec3(0.0, 0.0, 0.0);
  emission_11 = tmpvar_14;
  emission_11.xyz = (emission_11.xyz + (tmpvar_4.xyz * xlv_TEXCOORD5));
  outDiffuse_1.xyz = outDiffuseOcclusion_9.xyz;
  outEmission_2.w = emission_11.w;
  outEmission_2.xyz = exp2(-(emission_11.xyz));
  outDiffuse_1.w = 1.0;
  gl_FragData[0] = outDiffuse_1;
  gl_FragData[1] = vec4(0.0, 0.0, 0.0, 0.0);
  gl_FragData[2] = outNormal_10;
  gl_FragData[3] = outEmission_2;
}


#endif
"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"#ifdef VERTEX
#version 300 es
uniform 	mediump vec4 unity_SHAr;
uniform 	mediump vec4 unity_SHAg;
uniform 	mediump vec4 unity_SHAb;
uniform 	mediump vec4 unity_SHBr;
uniform 	mediump vec4 unity_SHBg;
uniform 	mediump vec4 unity_SHBb;
uniform 	mediump vec4 unity_SHC;
uniform 	mat4x4 glstate_matrix_mvp;
uniform 	mat4x4 _Object2World;
uniform 	mat4x4 _World2Object;
uniform 	vec4 unity_WorldTransformParams;
uniform 	vec4 _MainTex_ST;
in highp vec4 in_POSITION0;
in highp vec4 in_TANGENT0;
in highp vec3 in_NORMAL0;
in highp vec4 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
out highp vec4 vs_TEXCOORD1;
out highp vec4 vs_TEXCOORD2;
out highp vec4 vs_TEXCOORD3;
out highp vec4 vs_TEXCOORD4;
out mediump vec3 vs_TEXCOORD5;
vec4 u_xlat0;
vec4 u_xlat1;
mediump vec4 u_xlat16_1;
lowp vec3 u_xlat10_2;
vec3 u_xlat3;
mediump vec3 u_xlat16_4;
mediump vec3 u_xlat16_5;
float u_xlat19;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat0.x = in_NORMAL0.x * _World2Object[0].x;
    u_xlat0.y = in_NORMAL0.x * _World2Object[1].x;
    u_xlat0.zw = in_NORMAL0.xx * _World2Object[2].xx;
    u_xlat1.x = in_NORMAL0.y * _World2Object[0].y;
    u_xlat1.y = in_NORMAL0.y * _World2Object[1].y;
    u_xlat1.zw = in_NORMAL0.yy * _World2Object[2].yy;
    u_xlat0 = u_xlat0 + u_xlat1;
    u_xlat1.x = in_NORMAL0.z * _World2Object[0].z;
    u_xlat1.y = in_NORMAL0.z * _World2Object[1].z;
    u_xlat1.zw = in_NORMAL0.zz * _World2Object[2].zz;
    u_xlat0 = u_xlat0 + u_xlat1;
    u_xlat1.x = dot(u_xlat0.xyw, u_xlat0.xyw);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat0 = u_xlat0.xywz * u_xlat1.xxxx;
    u_xlat1.xyz = in_TANGENT0.yyy * _Object2World[1].yzx;
    u_xlat1.xyz = _Object2World[0].yzx * in_TANGENT0.xxx + u_xlat1.xyz;
    u_xlat1.xyz = _Object2World[2].yzx * in_TANGENT0.zzz + u_xlat1.xyz;
    u_xlat19 = dot(u_xlat1.xyz, u_xlat1.xyz);
    u_xlat19 = inversesqrt(u_xlat19);
    u_xlat1.xyz = vec3(u_xlat19) * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.zxy * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.yzx * u_xlat1.yzx + (-u_xlat10_2.xyz);
    u_xlat19 = in_TANGENT0.w * unity_WorldTransformParams.w;
    u_xlat10_2.xyz = vec3(u_xlat19) * u_xlat10_2.xyz;
    vs_TEXCOORD1.y = u_xlat10_2.x;
    u_xlat3.xyz = in_POSITION0.yyy * _Object2World[1].xyz;
    u_xlat3.xyz = _Object2World[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[3].xyz * in_POSITION0.www + u_xlat3.xyz;
    vs_TEXCOORD1.w = u_xlat3.x;
    vs_TEXCOORD1.z = u_xlat0.x;
    vs_TEXCOORD1.x = u_xlat1.z;
    vs_TEXCOORD2.x = u_xlat1.x;
    vs_TEXCOORD3.x = u_xlat1.y;
    vs_TEXCOORD2.w = u_xlat3.y;
    vs_TEXCOORD3.w = u_xlat3.z;
    vs_TEXCOORD2.z = u_xlat0.y;
    vs_TEXCOORD2.y = u_xlat10_2.y;
    vs_TEXCOORD3.y = u_xlat10_2.z;
    vs_TEXCOORD3.z = u_xlat0.z;
    vs_TEXCOORD4 = vec4(0.0, 0.0, 0.0, 0.0);
    u_xlat16_4.x = u_xlat0.y * u_xlat0.y;
    u_xlat16_4.x = u_xlat0.x * u_xlat0.x + (-u_xlat16_4.x);
    u_xlat16_1 = u_xlat0.yzwx * u_xlat0.xywz;
    u_xlat16_5.x = dot(unity_SHBr, u_xlat16_1);
    u_xlat16_5.y = dot(unity_SHBg, u_xlat16_1);
    u_xlat16_5.z = dot(unity_SHBb, u_xlat16_1);
    u_xlat16_4.xyz = unity_SHC.xyz * u_xlat16_4.xxx + u_xlat16_5.xyz;
    u_xlat0.w = 1.0;
    u_xlat16_5.x = dot(unity_SHAr, u_xlat0);
    u_xlat16_5.y = dot(unity_SHAg, u_xlat0);
    u_xlat16_5.z = dot(unity_SHAb, u_xlat0);
    u_xlat16_4.xyz = u_xlat16_4.xyz + u_xlat16_5.xyz;
    u_xlat16_4.xyz = max(u_xlat16_4.xyz, vec3(0.0, 0.0, 0.0));
    u_xlat0.xyz = log2(u_xlat16_4.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
    u_xlat0.xyz = exp2(u_xlat0.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
    vs_TEXCOORD5.xyz = u_xlat0.xyz;
    return;
}
#endif
#ifdef FRAGMENT
#version 300 es
precision highp int;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _BumpMap;
in highp vec2 vs_TEXCOORD0;
in highp vec4 vs_TEXCOORD1;
in highp vec4 vs_TEXCOORD2;
in highp vec4 vs_TEXCOORD3;
in mediump vec3 vs_TEXCOORD5;
layout(location = 0) out mediump vec4 SV_Target0;
layout(location = 1) out mediump vec4 SV_Target1;
layout(location = 2) out mediump vec4 SV_Target2;
layout(location = 3) out mediump vec4 SV_Target3;
vec3 u_xlat0;
lowp vec3 u_xlat10_0;
mediump vec3 u_xlat16_1;
lowp vec3 u_xlat10_2;
void main()
{
    SV_Target0.w = 1.0;
    u_xlat10_0.xyz = texture(_MainTex, vs_TEXCOORD0.xy).xyz;
    SV_Target0.xyz = u_xlat10_0.xyz;
    u_xlat16_1.xyz = u_xlat10_0.xyz * vs_TEXCOORD5.xyz;
    SV_Target3.xyz = exp2((-u_xlat16_1.xyz));
    SV_Target1 = vec4(0.0, 0.0, 0.0, 0.0);
    u_xlat10_0.xyz = texture(_BumpMap, vs_TEXCOORD0.xy).xyz;
    u_xlat10_2.xyz = u_xlat10_0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
    u_xlat0.x = dot(vs_TEXCOORD1.xyz, u_xlat10_2.xyz);
    u_xlat0.y = dot(vs_TEXCOORD2.xyz, u_xlat10_2.xyz);
    u_xlat0.z = dot(vs_TEXCOORD3.xyz, u_xlat10_2.xyz);
    u_xlat10_2.xyz = u_xlat0.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.5, 0.5, 0.5);
    SV_Target2.xyz = u_xlat10_2.xyz;
    SV_Target2.w = 1.0;
    SV_Target3.w = 1.0;
    return;
}
#endif
"
}
SubProgram "gles " {
// Stats: 15 math, 2 textures
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
"#version 100

#ifdef VERTEX
attribute vec4 _glesTANGENT;
attribute vec4 _glesVertex;
attribute vec3 _glesNormal;
attribute vec4 _glesMultiTexCoord0;
uniform mediump vec4 unity_SHAr;
uniform mediump vec4 unity_SHAg;
uniform mediump vec4 unity_SHAb;
uniform mediump vec4 unity_SHBr;
uniform mediump vec4 unity_SHBg;
uniform mediump vec4 unity_SHBb;
uniform mediump vec4 unity_SHC;
uniform highp mat4 glstate_matrix_mvp;
uniform highp mat4 _Object2World;
uniform highp mat4 _World2Object;
uniform highp vec4 unity_WorldTransformParams;
uniform highp vec4 _MainTex_ST;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying highp vec4 xlv_TEXCOORD4;
varying mediump vec3 xlv_TEXCOORD5;
void main ()
{
  lowp float tangentSign_1;
  lowp vec3 worldTangent_2;
  lowp vec3 worldNormal_3;
  highp vec4 tmpvar_4;
  highp vec3 tmpvar_5;
  tmpvar_5 = (_Object2World * _glesVertex).xyz;
  highp vec4 v_6;
  v_6.x = _World2Object[0].x;
  v_6.y = _World2Object[1].x;
  v_6.z = _World2Object[2].x;
  v_6.w = _World2Object[3].x;
  highp vec4 v_7;
  v_7.x = _World2Object[0].y;
  v_7.y = _World2Object[1].y;
  v_7.z = _World2Object[2].y;
  v_7.w = _World2Object[3].y;
  highp vec4 v_8;
  v_8.x = _World2Object[0].z;
  v_8.y = _World2Object[1].z;
  v_8.z = _World2Object[2].z;
  v_8.w = _World2Object[3].z;
  highp vec3 tmpvar_9;
  tmpvar_9 = normalize(((
    (v_6.xyz * _glesNormal.x)
   + 
    (v_7.xyz * _glesNormal.y)
  ) + (v_8.xyz * _glesNormal.z)));
  worldNormal_3 = tmpvar_9;
  highp mat3 tmpvar_10;
  tmpvar_10[0] = _Object2World[0].xyz;
  tmpvar_10[1] = _Object2World[1].xyz;
  tmpvar_10[2] = _Object2World[2].xyz;
  highp vec3 tmpvar_11;
  tmpvar_11 = normalize((tmpvar_10 * _glesTANGENT.xyz));
  worldTangent_2 = tmpvar_11;
  highp float tmpvar_12;
  tmpvar_12 = (_glesTANGENT.w * unity_WorldTransformParams.w);
  tangentSign_1 = tmpvar_12;
  lowp vec3 tmpvar_13;
  tmpvar_13 = (((worldNormal_3.yzx * worldTangent_2.zxy) - (worldNormal_3.zxy * worldTangent_2.yzx)) * tangentSign_1);
  highp vec4 tmpvar_14;
  tmpvar_14.x = worldTangent_2.x;
  tmpvar_14.y = tmpvar_13.x;
  tmpvar_14.z = worldNormal_3.x;
  tmpvar_14.w = tmpvar_5.x;
  highp vec4 tmpvar_15;
  tmpvar_15.x = worldTangent_2.y;
  tmpvar_15.y = tmpvar_13.y;
  tmpvar_15.z = worldNormal_3.y;
  tmpvar_15.w = tmpvar_5.y;
  highp vec4 tmpvar_16;
  tmpvar_16.x = worldTangent_2.z;
  tmpvar_16.y = tmpvar_13.z;
  tmpvar_16.z = worldNormal_3.z;
  tmpvar_16.w = tmpvar_5.z;
  tmpvar_4.zw = vec2(0.0, 0.0);
  tmpvar_4.xy = vec2(0.0, 0.0);
  mediump vec3 normal_17;
  normal_17 = worldNormal_3;
  mediump vec4 tmpvar_18;
  tmpvar_18.w = 1.0;
  tmpvar_18.xyz = normal_17;
  mediump vec3 res_19;
  mediump vec3 x_20;
  x_20.x = dot (unity_SHAr, tmpvar_18);
  x_20.y = dot (unity_SHAg, tmpvar_18);
  x_20.z = dot (unity_SHAb, tmpvar_18);
  mediump vec3 x1_21;
  mediump vec4 tmpvar_22;
  tmpvar_22 = (normal_17.xyzz * normal_17.yzzx);
  x1_21.x = dot (unity_SHBr, tmpvar_22);
  x1_21.y = dot (unity_SHBg, tmpvar_22);
  x1_21.z = dot (unity_SHBb, tmpvar_22);
  res_19 = (x_20 + (x1_21 + (unity_SHC.xyz * 
    ((normal_17.x * normal_17.x) - (normal_17.y * normal_17.y))
  )));
  res_19 = max (((1.055 * 
    pow (max (res_19, vec3(0.0, 0.0, 0.0)), vec3(0.4166667, 0.4166667, 0.4166667))
  ) - 0.055), vec3(0.0, 0.0, 0.0));
  gl_Position = (glstate_matrix_mvp * _glesVertex);
  xlv_TEXCOORD0 = ((_glesMultiTexCoord0.xy * _MainTex_ST.xy) + _MainTex_ST.zw);
  xlv_TEXCOORD1 = tmpvar_14;
  xlv_TEXCOORD2 = tmpvar_15;
  xlv_TEXCOORD3 = tmpvar_16;
  xlv_TEXCOORD4 = tmpvar_4;
  xlv_TEXCOORD5 = max (vec3(0.0, 0.0, 0.0), res_19);
}


#endif
#ifdef FRAGMENT
#extension GL_EXT_draw_buffers : enable
uniform sampler2D _MainTex;
uniform sampler2D _BumpMap;
varying highp vec2 xlv_TEXCOORD0;
varying highp vec4 xlv_TEXCOORD1;
varying highp vec4 xlv_TEXCOORD2;
varying highp vec4 xlv_TEXCOORD3;
varying mediump vec3 xlv_TEXCOORD5;
void main ()
{
  mediump vec4 outDiffuse_1;
  lowp vec3 worldN_2;
  lowp vec4 tmpvar_3;
  tmpvar_3 = texture2D (_MainTex, xlv_TEXCOORD0);
  lowp vec3 tmpvar_4;
  tmpvar_4 = ((texture2D (_BumpMap, xlv_TEXCOORD0).xyz * 2.0) - 1.0);
  highp float tmpvar_5;
  tmpvar_5 = dot (xlv_TEXCOORD1.xyz, tmpvar_4);
  worldN_2.x = tmpvar_5;
  highp float tmpvar_6;
  tmpvar_6 = dot (xlv_TEXCOORD2.xyz, tmpvar_4);
  worldN_2.y = tmpvar_6;
  highp float tmpvar_7;
  tmpvar_7 = dot (xlv_TEXCOORD3.xyz, tmpvar_4);
  worldN_2.z = tmpvar_7;
  mediump vec4 outDiffuseOcclusion_8;
  mediump vec4 outNormal_9;
  mediump vec4 emission_10;
  lowp vec4 tmpvar_11;
  tmpvar_11.w = 1.0;
  tmpvar_11.xyz = tmpvar_3.xyz;
  outDiffuseOcclusion_8 = tmpvar_11;
  lowp vec4 tmpvar_12;
  tmpvar_12.w = 1.0;
  tmpvar_12.xyz = ((worldN_2 * 0.5) + 0.5);
  outNormal_9 = tmpvar_12;
  lowp vec4 tmpvar_13;
  tmpvar_13.w = 1.0;
  tmpvar_13.xyz = vec3(0.0, 0.0, 0.0);
  emission_10 = tmpvar_13;
  emission_10.xyz = (emission_10.xyz + (tmpvar_3.xyz * xlv_TEXCOORD5));
  outDiffuse_1.xyz = outDiffuseOcclusion_8.xyz;
  outDiffuse_1.w = 1.0;
  gl_FragData[0] = outDiffuse_1;
  gl_FragData[1] = vec4(0.0, 0.0, 0.0, 0.0);
  gl_FragData[2] = outNormal_9;
  gl_FragData[3] = emission_10;
}


#endif
"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
"#ifdef VERTEX
#version 300 es
uniform 	mediump vec4 unity_SHAr;
uniform 	mediump vec4 unity_SHAg;
uniform 	mediump vec4 unity_SHAb;
uniform 	mediump vec4 unity_SHBr;
uniform 	mediump vec4 unity_SHBg;
uniform 	mediump vec4 unity_SHBb;
uniform 	mediump vec4 unity_SHC;
uniform 	mat4x4 glstate_matrix_mvp;
uniform 	mat4x4 _Object2World;
uniform 	mat4x4 _World2Object;
uniform 	vec4 unity_WorldTransformParams;
uniform 	vec4 _MainTex_ST;
in highp vec4 in_POSITION0;
in highp vec4 in_TANGENT0;
in highp vec3 in_NORMAL0;
in highp vec4 in_TEXCOORD0;
out highp vec2 vs_TEXCOORD0;
out highp vec4 vs_TEXCOORD1;
out highp vec4 vs_TEXCOORD2;
out highp vec4 vs_TEXCOORD3;
out highp vec4 vs_TEXCOORD4;
out mediump vec3 vs_TEXCOORD5;
vec4 u_xlat0;
vec4 u_xlat1;
mediump vec4 u_xlat16_1;
lowp vec3 u_xlat10_2;
vec3 u_xlat3;
mediump vec3 u_xlat16_4;
mediump vec3 u_xlat16_5;
float u_xlat19;
void main()
{
    u_xlat0 = in_POSITION0.yyyy * glstate_matrix_mvp[1];
    u_xlat0 = glstate_matrix_mvp[0] * in_POSITION0.xxxx + u_xlat0;
    u_xlat0 = glstate_matrix_mvp[2] * in_POSITION0.zzzz + u_xlat0;
    gl_Position = glstate_matrix_mvp[3] * in_POSITION0.wwww + u_xlat0;
    vs_TEXCOORD0.xy = in_TEXCOORD0.xy * _MainTex_ST.xy + _MainTex_ST.zw;
    u_xlat0.x = in_NORMAL0.x * _World2Object[0].x;
    u_xlat0.y = in_NORMAL0.x * _World2Object[1].x;
    u_xlat0.zw = in_NORMAL0.xx * _World2Object[2].xx;
    u_xlat1.x = in_NORMAL0.y * _World2Object[0].y;
    u_xlat1.y = in_NORMAL0.y * _World2Object[1].y;
    u_xlat1.zw = in_NORMAL0.yy * _World2Object[2].yy;
    u_xlat0 = u_xlat0 + u_xlat1;
    u_xlat1.x = in_NORMAL0.z * _World2Object[0].z;
    u_xlat1.y = in_NORMAL0.z * _World2Object[1].z;
    u_xlat1.zw = in_NORMAL0.zz * _World2Object[2].zz;
    u_xlat0 = u_xlat0 + u_xlat1;
    u_xlat1.x = dot(u_xlat0.xyw, u_xlat0.xyw);
    u_xlat1.x = inversesqrt(u_xlat1.x);
    u_xlat0 = u_xlat0.xywz * u_xlat1.xxxx;
    u_xlat1.xyz = in_TANGENT0.yyy * _Object2World[1].yzx;
    u_xlat1.xyz = _Object2World[0].yzx * in_TANGENT0.xxx + u_xlat1.xyz;
    u_xlat1.xyz = _Object2World[2].yzx * in_TANGENT0.zzz + u_xlat1.xyz;
    u_xlat19 = dot(u_xlat1.xyz, u_xlat1.xyz);
    u_xlat19 = inversesqrt(u_xlat19);
    u_xlat1.xyz = vec3(u_xlat19) * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.zxy * u_xlat1.xyz;
    u_xlat10_2.xyz = u_xlat0.yzx * u_xlat1.yzx + (-u_xlat10_2.xyz);
    u_xlat19 = in_TANGENT0.w * unity_WorldTransformParams.w;
    u_xlat10_2.xyz = vec3(u_xlat19) * u_xlat10_2.xyz;
    vs_TEXCOORD1.y = u_xlat10_2.x;
    u_xlat3.xyz = in_POSITION0.yyy * _Object2World[1].xyz;
    u_xlat3.xyz = _Object2World[0].xyz * in_POSITION0.xxx + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[2].xyz * in_POSITION0.zzz + u_xlat3.xyz;
    u_xlat3.xyz = _Object2World[3].xyz * in_POSITION0.www + u_xlat3.xyz;
    vs_TEXCOORD1.w = u_xlat3.x;
    vs_TEXCOORD1.z = u_xlat0.x;
    vs_TEXCOORD1.x = u_xlat1.z;
    vs_TEXCOORD2.x = u_xlat1.x;
    vs_TEXCOORD3.x = u_xlat1.y;
    vs_TEXCOORD2.w = u_xlat3.y;
    vs_TEXCOORD3.w = u_xlat3.z;
    vs_TEXCOORD2.z = u_xlat0.y;
    vs_TEXCOORD2.y = u_xlat10_2.y;
    vs_TEXCOORD3.y = u_xlat10_2.z;
    vs_TEXCOORD3.z = u_xlat0.z;
    vs_TEXCOORD4 = vec4(0.0, 0.0, 0.0, 0.0);
    u_xlat16_4.x = u_xlat0.y * u_xlat0.y;
    u_xlat16_4.x = u_xlat0.x * u_xlat0.x + (-u_xlat16_4.x);
    u_xlat16_1 = u_xlat0.yzwx * u_xlat0.xywz;
    u_xlat16_5.x = dot(unity_SHBr, u_xlat16_1);
    u_xlat16_5.y = dot(unity_SHBg, u_xlat16_1);
    u_xlat16_5.z = dot(unity_SHBb, u_xlat16_1);
    u_xlat16_4.xyz = unity_SHC.xyz * u_xlat16_4.xxx + u_xlat16_5.xyz;
    u_xlat0.w = 1.0;
    u_xlat16_5.x = dot(unity_SHAr, u_xlat0);
    u_xlat16_5.y = dot(unity_SHAg, u_xlat0);
    u_xlat16_5.z = dot(unity_SHAb, u_xlat0);
    u_xlat16_4.xyz = u_xlat16_4.xyz + u_xlat16_5.xyz;
    u_xlat16_4.xyz = max(u_xlat16_4.xyz, vec3(0.0, 0.0, 0.0));
    u_xlat0.xyz = log2(u_xlat16_4.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(0.416666657, 0.416666657, 0.416666657);
    u_xlat0.xyz = exp2(u_xlat0.xyz);
    u_xlat0.xyz = u_xlat0.xyz * vec3(1.05499995, 1.05499995, 1.05499995) + vec3(-0.0549999997, -0.0549999997, -0.0549999997);
    u_xlat0.xyz = max(u_xlat0.xyz, vec3(0.0, 0.0, 0.0));
    vs_TEXCOORD5.xyz = u_xlat0.xyz;
    return;
}
#endif
#ifdef FRAGMENT
#version 300 es
precision highp int;
uniform lowp sampler2D _MainTex;
uniform lowp sampler2D _BumpMap;
in highp vec2 vs_TEXCOORD0;
in highp vec4 vs_TEXCOORD1;
in highp vec4 vs_TEXCOORD2;
in highp vec4 vs_TEXCOORD3;
in mediump vec3 vs_TEXCOORD5;
layout(location = 0) out mediump vec4 SV_Target0;
layout(location = 1) out mediump vec4 SV_Target1;
layout(location = 2) out mediump vec4 SV_Target2;
layout(location = 3) out mediump vec4 SV_Target3;
vec3 u_xlat0;
lowp vec3 u_xlat10_0;
lowp vec3 u_xlat10_1;
void main()
{
    SV_Target0.w = 1.0;
    u_xlat10_0.xyz = texture(_MainTex, vs_TEXCOORD0.xy).xyz;
    SV_Target0.xyz = u_xlat10_0.xyz;
    SV_Target3.xyz = u_xlat10_0.xyz * vs_TEXCOORD5.xyz;
    SV_Target1 = vec4(0.0, 0.0, 0.0, 0.0);
    u_xlat10_0.xyz = texture(_BumpMap, vs_TEXCOORD0.xy).xyz;
    u_xlat10_1.xyz = u_xlat10_0.xyz * vec3(2.0, 2.0, 2.0) + vec3(-1.0, -1.0, -1.0);
    u_xlat0.x = dot(vs_TEXCOORD1.xyz, u_xlat10_1.xyz);
    u_xlat0.y = dot(vs_TEXCOORD2.xyz, u_xlat10_1.xyz);
    u_xlat0.z = dot(vs_TEXCOORD3.xyz, u_xlat10_1.xyz);
    u_xlat10_1.xyz = u_xlat0.xyz * vec3(0.5, 0.5, 0.5) + vec3(0.5, 0.5, 0.5);
    SV_Target2.xyz = u_xlat10_1.xyz;
    SV_Target2.w = 1.0;
    SV_Target3.w = 1.0;
    return;
}
#endif
"
}
}
Program "fp" {
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"// shader disassembly not supported on gles"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "HDR_LIGHT_PREPASS_OFF" }
"// shader disassembly not supported on gles3"
}
SubProgram "gles " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
"// shader disassembly not supported on gles"
}
SubProgram "gles3 " {
Keywords { "LIGHTMAP_OFF" "DIRLIGHTMAP_OFF" "DYNAMICLIGHTMAP_OFF" "UNITY_HDR_ON" }
"// shader disassembly not supported on gles3"
}
}
 }
}
Fallback "Mobile/Diffuse"
}