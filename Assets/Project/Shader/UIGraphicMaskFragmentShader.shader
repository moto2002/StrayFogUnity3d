﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
Shader "Game/UI/GraphicMask/Fragment"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)

		_StencilComp ("Stencil Comparison", Float) = 8
		_Stencil ("Stencil ID", Float) = 0
		_StencilOp ("Stencil Operation", Float) = 0
		_StencilWriteMask ("Stencil Write Mask", Float) = 255
		_StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15

		[Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
		Lighting Off
		ZWrite Off
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha
		ColorMask [_ColorMask]

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			#include "UnityUI.cginc"

			#pragma multi_compile __ UNITY_UI_ALPHACLIP
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
				float4 worldPosition : TEXCOORD1;
			};

			sampler2D _MainTex;
			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			float4 _ClipRect;
			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.worldPosition = IN.vertex;
				OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

				OUT.texcoord = IN.texcoord;
				
				#ifdef UNITY_HALF_TEXEL_OFFSET
				OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
				#endif
				
				OUT.color = IN.color * _Color;
				return OUT;
			}
			
			//图形遮罩分类
			int _graphicMaskClassify;
			//绘制精灵填充分类
			int _graphicSpriteFillClassify;
			//是否没有主图片
			bool _isNoneMainTex;
            //遮罩要支持多少个，这里就添加多少个，建议最多不超过6个
            //遮罩0
            sampler2D _GraphicTex0;
            //屏幕与遮罩长宽
            float4 _SceneGraphicWh0;
            //遮罩最小最大uv坐标
            float4 _GraphicUvMinMax0;
            //遮罩最小最大uv坐标裁剪
            float4 _GraphicUvMinMaxClip0;
            //遮罩矩形(0,0)(1,1)相对于屏幕uv坐标
            float4 _GraphicUvMaskForScene0;
            //遮罩Sliced绘制
            int _isGraphicSliced0;
            //遮罩矩形Sliced Border
            float4 _GraphicRectSlicedBorder0;
            //遮罩图Sliced Border缩放比率
            float4 _GraphicUvSlicedBorderRatio0;
			//遮罩MainTex参数
			half4 _graphicMainTex0;

            //遮罩1
            sampler2D _GraphicTex1;
            //屏幕与遮罩长宽
            float4 _SceneGraphicWh1;
            //遮罩最小最大uv坐标
            float4 _GraphicUvMinMax1;
            //遮罩最小最大uv坐标裁剪
            float4 _GraphicUvMinMaxClip1;
            //遮罩矩形(0,0)(1,1)相对于屏幕uv坐标
            float4 _GraphicUvMaskForScene1;
            //遮罩Sliced绘制
            int _isGraphicSliced1;
            //遮罩矩形Sliced Border
            float4 _GraphicRectSlicedBorder1;
             //遮罩图Sliced Border缩放比率
            float4 _GraphicUvSlicedBorderRatio1;
			//遮罩MainTex参数
			half4 _graphicMainTex1;

			//遮罩2
            sampler2D _GraphicTex2;
            //屏幕与遮罩长宽
            float4 _SceneGraphicWh2;
            //遮罩最小最大uv坐标
            float4 _GraphicUvMinMax2;
            //遮罩最小最大uv坐标裁剪
            float4 _GraphicUvMinMaxClip2;
            //遮罩矩形(0,0)(1,1)相对于屏幕uv坐标
            float4 _GraphicUvMaskForScene2;
            //遮罩Sliced绘制
            int _isGraphicSliced2;
            //遮罩矩形Sliced Border
            float4 _GraphicRectSlicedBorder2;
             //遮罩图Sliced Border缩放比率
            float4 _GraphicUvSlicedBorderRatio2;
			//遮罩MainTex参数
			half4 _graphicMainTex2;

			//遮罩3
            sampler2D _GraphicTex3;
            //屏幕与遮罩长宽
            float4 _SceneGraphicWh3;
            //遮罩最小最大uv坐标
            float4 _GraphicUvMinMax3;
            //遮罩最小最大uv坐标裁剪
            float4 _GraphicUvMinMaxClip3;
            //遮罩矩形(0,0)(1,1)相对于屏幕uv坐标
            float4 _GraphicUvMaskForScene3;
            //遮罩Sliced绘制
            int _isGraphicSliced3;
            //遮罩矩形Sliced Border
            float4 _GraphicRectSlicedBorder3;
             //遮罩图Sliced Border缩放比率
            float4 _GraphicUvSlicedBorderRatio3;
			//遮罩MainTex参数
			half4 _graphicMainTex3;

			//遮罩4
            sampler2D _GraphicTex4;
            //屏幕与遮罩长宽
            float4 _SceneGraphicWh4;
            //遮罩最小最大uv坐标
            float4 _GraphicUvMinMax4;
            //遮罩最小最大uv坐标裁剪
            float4 _GraphicUvMinMaxClip4;
            //遮罩矩形(0,0)(1,1)相对于屏幕uv坐标
            float4 _GraphicUvMaskForScene4;
            //遮罩Sliced绘制
            int _isGraphicSliced4;
            //遮罩矩形Sliced Border
            float4 _GraphicRectSlicedBorder4;
             //遮罩图Sliced Border缩放比率
            float4 _GraphicUvSlicedBorderRatio4;
			//遮罩MainTex参数
			half4 _graphicMainTex4;

			//遮罩5
            sampler2D _GraphicTex5;
            //屏幕与遮罩长宽
            float4 _SceneGraphicWh5;
            //遮罩最小最大uv坐标
            float4 _GraphicUvMinMax5;
            //遮罩最小最大uv坐标裁剪
            float4 _GraphicUvMinMaxClip5;
            //遮罩矩形(0,0)(1,1)相对于屏幕uv坐标
            float4 _GraphicUvMaskForScene5;
            //遮罩Sliced绘制
            int _isGraphicSliced5;
            //遮罩矩形Sliced Border
            float4 _GraphicRectSlicedBorder5;
             //遮罩图Sliced Border缩放比率
            float4 _GraphicUvSlicedBorderRatio5;
			//遮罩MainTex参数
			half4 _graphicMainTex5;

			//填充开始
            /*
            x=>u最小值，z=>u最大值 ，y=>v最小值，y=>v最大值
            v
            |     xw---------zw
            |       |               |
            |     xy----------zy
            ------------------------u
            */
            //遮罩区内颜色值
            half4 fragMaskGraphic(v2f _in,sampler2D _graphicTex,float4 _sceneGraphicWh,
            float4 _graphicUvMinMax,float4 _graphicUvMinMaxClip,float4 _graphicUvMaskForScene,
            int _isGraphicSliced,float4 _GraphicRectSlicedBorder,float4 _GraphicUvSlicedBorderRatio)
            {				
                //屏幕点
                half2 texuv  = _in.texcoord;
				//graphicColor
				half4 graphicTex = half4(texuv,0,0);
                //如果点在要绘制的遮罩框内才进行像素计算
                if(texuv.x >= _graphicUvMaskForScene.x && texuv.x <= _graphicUvMaskForScene.z
                && texuv.y >= _graphicUvMaskForScene.y && texuv.y <= _graphicUvMaskForScene.w)
                {					
                    //长宽比
                    float2 ratiouv = float2(_sceneGraphicWh.x/_sceneGraphicWh.z,_sceneGraphicWh.y/_sceneGraphicWh.w);
                    //矩形参数
                    float4 rectParams = float4(_graphicUvMaskForScene.x,_graphicUvMaskForScene.y, _graphicUvMaskForScene.z - _graphicUvMaskForScene.x,_graphicUvMaskForScene.w - _graphicUvMaskForScene.y);
                    //遮罩图参数
                    float4 texParams= float4(_graphicUvMinMax.x,_graphicUvMinMax.y,_graphicUvMinMax.z - _graphicUvMinMax.x,_graphicUvMinMax.w - _graphicUvMinMax.y);
					
					if(_isGraphicSliced == 1)
                    {
                        //_GraphicUvSlicedBorder 九宫uv值,判定当前uv在九宫的哪一格,按当前格子的缩放进行uv读取
                        //转换到遮罩(0,0)坐标
                        texuv -=_graphicUvMaskForScene.xy;
                        //由屏幕坐标系转到遮罩坐标系
                        texuv *=ratiouv;
                        texuv +=_graphicUvMinMaxClip.xy;
                        if(texuv.x > _GraphicRectSlicedBorder.x && texuv.x < _GraphicRectSlicedBorder.z && texuv.y > _GraphicRectSlicedBorder.y && texuv.y < _GraphicRectSlicedBorder.w)
                        {
							//九宫格中间部分
                            graphicTex.a = tex2D(_graphicTex,texuv).a;
                        }
                        else
                        {                            
                            //左下角               
                            if(texuv.x< _GraphicRectSlicedBorder.x &&
                               texuv.y <_GraphicRectSlicedBorder.y)
                            {
                                //坐标转换到左下角坐标系(0,1)
                                texuv /= _GraphicRectSlicedBorder.xy;
                                //坐标转换到Texture坐标系
                                texuv *= _GraphicUvSlicedBorderRatio.xy;
                                //遮罩坐标系转到Texture坐标系
                                texuv = texuv * texParams.zw + texParams.xy;
                                //遮罩在屏幕的矩形裁剪
                                //texuv +=_graphicUvMinMaxClip.xy * texParams.zw;
                                graphicTex.a = tex2D(_graphicTex,texuv).a;
                            }
                            //左上角
                            else if(texuv.x< _GraphicRectSlicedBorder.x &&
                             texuv.y >_GraphicRectSlicedBorder.w)
                            {                            
                                //相对于左上角为原点的坐标
                                texuv.y =1 - texuv.y;
                                //坐标转换到左上角坐标系(0,1)
                                texuv.x /= _GraphicRectSlicedBorder.x;
                                texuv.y /= (1 - _GraphicRectSlicedBorder.w);
                                //坐标转换到Texture坐标系
                                texuv *= _GraphicUvSlicedBorderRatio.xw;
                                texuv.y = 1 - texuv.y;
                                //遮罩坐标系转到Texture坐标系
                                texuv = texuv * texParams.zw + texParams.xy;
                                //遮罩在屏幕的矩形裁剪
                                //texuv +=_graphicUvMinMaxClip.xy * texParams.zw;
                                graphicTex.a = tex2D(_graphicTex,texuv).a;
                            }
                            //右上角
                            else if(texuv.x > _GraphicRectSlicedBorder.z &&
                             texuv.y >_GraphicRectSlicedBorder.w)
                            {                            
                                //相对于左上角为原点的坐标
                                texuv =1 - texuv;
                                //坐标转换到左上角坐标系(0,1)
                                texuv /= (1 -_GraphicRectSlicedBorder.zw);
                                //坐标转换到Texture坐标系
                                texuv *= _GraphicUvSlicedBorderRatio.zw;
                                texuv = 1 - texuv;
                                //遮罩坐标系转到Texture坐标系
                                texuv = texuv * texParams.zw + texParams.xy;
                                //遮罩在屏幕的矩形裁剪
                                //texuv +=_graphicUvMinMaxClip.xy * texParams.zw;
                                graphicTex.a = tex2D(_graphicTex,texuv).a;
                            }
                             //右下角
                            else if(texuv.x > _GraphicRectSlicedBorder.z &&
                             texuv.y < _GraphicRectSlicedBorder.y)
                            {                            
                                //相对于左上角为原点的坐标
                                texuv.x =1 - texuv.x;
                                //坐标转换到左上角坐标系(0,1)
                                texuv.x /= (1 -_GraphicRectSlicedBorder.z);
                                texuv.y /= _GraphicRectSlicedBorder.y;
                                //坐标转换到Texture坐标系
                                texuv *= _GraphicUvSlicedBorderRatio.zy;
                                texuv.x = 1 - texuv.x;
                                //遮罩坐标系转到Texture坐标系
                                texuv = texuv * texParams.zw + texParams.xy;
                                //遮罩在屏幕的矩形裁剪
                                //texuv +=_graphicUvMinMaxClip.xy * texParams.zw;
                                graphicTex.a = tex2D(_graphicTex,texuv).a;
                            }
                            //左边
                            else if(texuv.x< _GraphicRectSlicedBorder.x &&
                               texuv.y > _GraphicRectSlicedBorder.y && texuv.y < _GraphicRectSlicedBorder.w)
                            {
                                //相对于左边左下角为原点的坐标
                                texuv.y -= _GraphicRectSlicedBorder.y;
                                //坐标转换到左下角坐标系(0,1)
                                texuv.x /= _GraphicRectSlicedBorder.x;
                                texuv.y /= _GraphicRectSlicedBorder.w - _GraphicRectSlicedBorder.y;                                
                                //坐标转换到Texture坐标系
                                texuv.x *= _GraphicUvSlicedBorderRatio.x;
                                texuv.y *= 1 - _GraphicUvSlicedBorderRatio.w - _GraphicUvSlicedBorderRatio.y;
                                //坐标补上左下角的y值
                                texuv.y += _GraphicUvSlicedBorderRatio.y;
                                //遮罩坐标系转到Texture坐标系
                                texuv = texuv * texParams.zw + texParams.xy;
                                //遮罩在屏幕的矩形裁剪
                                //texuv +=_graphicUvMinMaxClip.xy * texParams.zw;
                                graphicTex.a = tex2D(_graphicTex,texuv).a;
                            }
                            //右边
                            else if(texuv.x > _GraphicRectSlicedBorder.z &&
                               texuv.y > _GraphicRectSlicedBorder.y && texuv.y < _GraphicRectSlicedBorder.w)
                            {
                                //相对于右边右上角为原点的坐标
                                texuv = 1 - texuv;
                                texuv.y -= 1-_GraphicRectSlicedBorder.w;
                                //坐标转换到右上角坐标系(0,1)
                                texuv.x /= 1 - _GraphicRectSlicedBorder.z;
                                texuv.y /= _GraphicRectSlicedBorder.w - _GraphicRectSlicedBorder.y;                                
                                //坐标转换到Texture坐标系
                                texuv.x *= _GraphicUvSlicedBorderRatio.z;
                                texuv.y *= 1 - _GraphicUvSlicedBorderRatio.w - _GraphicUvSlicedBorderRatio.y;
                                //坐标补上左下角的y值
                                texuv.y += _GraphicUvSlicedBorderRatio.w;
                                texuv = 1-texuv;
                                //遮罩坐标系转到Texture坐标系
                                texuv = texuv * texParams.zw + texParams.xy;
                                //遮罩在屏幕的矩形裁剪
                                //texuv +=_graphicUvMinMaxClip.xy * texParams.zw;
                                graphicTex.a = tex2D(_graphicTex,texuv).a;
                            }
                            //下边
                            else if(texuv.x > _GraphicRectSlicedBorder.x && texuv.x < _GraphicRectSlicedBorder.z &&
                               texuv.y < _GraphicRectSlicedBorder.y)
                            {
                                //相对于下边左下角为原点的坐标
                                texuv.x -= _GraphicRectSlicedBorder.x;
                                //坐标转换到左下角坐标系(0,1)
                                texuv.x /=_GraphicRectSlicedBorder.z - _GraphicRectSlicedBorder.x;     
                                texuv.y /= _GraphicRectSlicedBorder.y;
                                //坐标转换到Texture坐标系
                                texuv.x *= 1 - _GraphicUvSlicedBorderRatio.z - _GraphicUvSlicedBorderRatio.x;
                                texuv.y *= _GraphicUvSlicedBorderRatio.y;
                                //坐标补上左下角的x值
                                texuv.x += _GraphicUvSlicedBorderRatio.x;
                                //遮罩坐标系转到Texture坐标系
                                texuv = texuv * texParams.zw + texParams.xy;
                                //遮罩在屏幕的矩形裁剪
                                //texuv +=_graphicUvMinMaxClip.xy * texParams.zw;
                                graphicTex.a = tex2D(_graphicTex,texuv).a;           
                            }
                             //上边
                            else if(texuv.x > _GraphicRectSlicedBorder.x && texuv.x < _GraphicRectSlicedBorder.z &&
                               texuv.y > _GraphicRectSlicedBorder.w)
                            {
                                //相对于上边右上角为原点的坐标
                                texuv = 1 - texuv;
                                texuv.x -= 1- _GraphicRectSlicedBorder.z;
                                texuv.y -= 1-_GraphicRectSlicedBorder.w;
                                //坐标转换到右上角坐标系(0,1)
                                texuv.x /= _GraphicRectSlicedBorder.z - _GraphicRectSlicedBorder.x;
                                texuv.y /= 1 -_GraphicRectSlicedBorder.w;
                                //坐标转换到Texture坐标系
                                texuv.x *= 1 - _GraphicUvSlicedBorderRatio.z - _GraphicUvSlicedBorderRatio.x;
                                texuv.y *= _GraphicUvSlicedBorderRatio.w;
                                //坐标补上右上角的xy值
                                texuv.x += _GraphicUvSlicedBorderRatio.z;
                                texuv.y += _GraphicUvSlicedBorderRatio.w;
                                texuv = 1-texuv;
                                //遮罩坐标系转到Texture坐标系
                                texuv = texuv * texParams.zw + texParams.xy;
                                //遮罩在屏幕的矩形裁剪
                                //texuv +=_graphicUvMinMaxClip.xy * texParams.zw;
                                graphicTex.a = tex2D(_graphicTex,texuv).a;
                            }                            
                        }
                    }
                    else
                    {
                        //转换到遮罩(0,0)坐标
                        texuv -=_graphicUvMaskForScene.xy;
                        //由屏幕坐标系转到遮罩坐标系
                        texuv *=ratiouv;
                        //遮罩在屏幕的矩形裁剪
                        texuv +=_graphicUvMinMaxClip.xy;
                        //遮罩坐标系转到Texture坐标系
                        texuv = texuv * texParams.zw + texParams.xy;
                        //遮罩在屏幕的矩形裁剪
                        //texuv +=_graphicUvMinMaxClip.xy * texParams.zw;
                        graphicTex.a = tex2D(_graphicTex,texuv).a;			
                    }                    
                }
				graphicTex.xy = texuv;
				graphicTex.a = 1- graphicTex.a;
				return graphicTex;
            }

			half4 fragMaskAloneFillColor(half2 _texcoord,half2 _graphicTexUV,half _alpha,half4 _color)
			{
				_alpha = 1-_alpha;
				half4 color=half4(0,0,0,0);
				if(_alpha > 0)
				{
					if(_graphicSpriteFillClassify == 1)
					{
						_texcoord = _graphicTexUV;
					}
					color = (tex2D(_MainTex, _texcoord) + _TextureSampleAdd) * _color;
				}
				return color;
			}

            fixed4 frag(v2f _in) : SV_Target
			{
				half4 color = half4(0,0,0,0);
				if (_graphicMaskClassify == 0)
				{
					//全屏填充
					color = (tex2D(_MainTex, _in.texcoord) + _TextureSampleAdd) * _in.color;
				}
				else if (_graphicMaskClassify == 1)
				{
					//仅填充遮罩
					_graphicMainTex0 = fragMaskGraphic(_in, _GraphicTex0, _SceneGraphicWh0, _GraphicUvMinMax0, _GraphicUvMinMaxClip0, _GraphicUvMaskForScene0, _isGraphicSliced0, _GraphicRectSlicedBorder0, _GraphicUvSlicedBorderRatio0);
					_graphicMainTex1 = fragMaskGraphic(_in, _GraphicTex1, _SceneGraphicWh1, _GraphicUvMinMax1, _GraphicUvMinMaxClip1, _GraphicUvMaskForScene1, _isGraphicSliced1, _GraphicRectSlicedBorder1, _GraphicUvSlicedBorderRatio1);
					_graphicMainTex2 = fragMaskGraphic(_in, _GraphicTex2, _SceneGraphicWh2, _GraphicUvMinMax2, _GraphicUvMinMaxClip2, _GraphicUvMaskForScene2, _isGraphicSliced2, _GraphicRectSlicedBorder2, _GraphicUvSlicedBorderRatio2);
					_graphicMainTex3 = fragMaskGraphic(_in, _GraphicTex3, _SceneGraphicWh3, _GraphicUvMinMax3, _GraphicUvMinMaxClip3, _GraphicUvMaskForScene3, _isGraphicSliced3, _GraphicRectSlicedBorder3, _GraphicUvSlicedBorderRatio3);
					_graphicMainTex4 = fragMaskGraphic(_in, _GraphicTex4, _SceneGraphicWh4, _GraphicUvMinMax4, _GraphicUvMinMaxClip4, _GraphicUvMaskForScene4, _isGraphicSliced4, _GraphicRectSlicedBorder4, _GraphicUvSlicedBorderRatio4);
					_graphicMainTex5 = fragMaskGraphic(_in, _GraphicTex5, _SceneGraphicWh5, _GraphicUvMinMax5, _GraphicUvMinMaxClip5, _GraphicUvMaskForScene5, _isGraphicSliced5, _GraphicRectSlicedBorder5, _GraphicUvSlicedBorderRatio5);
					color += fragMaskAloneFillColor(_in.texcoord, _graphicMainTex0.xy, _graphicMainTex0.a, _in.color);
					color += fragMaskAloneFillColor(_in.texcoord, _graphicMainTex1.xy, _graphicMainTex1.a, _in.color);
					color += fragMaskAloneFillColor(_in.texcoord, _graphicMainTex2.xy, _graphicMainTex2.a, _in.color);
					color += fragMaskAloneFillColor(_in.texcoord, _graphicMainTex3.xy, _graphicMainTex3.a, _in.color);
					color += fragMaskAloneFillColor(_in.texcoord, _graphicMainTex4.xy, _graphicMainTex4.a, _in.color);
					color += fragMaskAloneFillColor(_in.texcoord, _graphicMainTex5.xy, _graphicMainTex5.a, _in.color);
				}
				else
				{
					//除遮罩外都填充
					_graphicMainTex0 = fragMaskGraphic(_in, _GraphicTex0, _SceneGraphicWh0, _GraphicUvMinMax0, _GraphicUvMinMaxClip0, _GraphicUvMaskForScene0, _isGraphicSliced0, _GraphicRectSlicedBorder0, _GraphicUvSlicedBorderRatio0);
					_graphicMainTex1 = fragMaskGraphic(_in, _GraphicTex1, _SceneGraphicWh1, _GraphicUvMinMax1, _GraphicUvMinMaxClip1, _GraphicUvMaskForScene1, _isGraphicSliced1, _GraphicRectSlicedBorder1, _GraphicUvSlicedBorderRatio1);
					_graphicMainTex2 = fragMaskGraphic(_in, _GraphicTex2, _SceneGraphicWh2, _GraphicUvMinMax2, _GraphicUvMinMaxClip2, _GraphicUvMaskForScene2, _isGraphicSliced2, _GraphicRectSlicedBorder2, _GraphicUvSlicedBorderRatio2);
					_graphicMainTex3 = fragMaskGraphic(_in, _GraphicTex3, _SceneGraphicWh3, _GraphicUvMinMax3, _GraphicUvMinMaxClip3, _GraphicUvMaskForScene3, _isGraphicSliced3, _GraphicRectSlicedBorder3, _GraphicUvSlicedBorderRatio3);
					_graphicMainTex4 = fragMaskGraphic(_in, _GraphicTex4, _SceneGraphicWh4, _GraphicUvMinMax4, _GraphicUvMinMaxClip4, _GraphicUvMaskForScene4, _isGraphicSliced4, _GraphicRectSlicedBorder4, _GraphicUvSlicedBorderRatio4);
					_graphicMainTex5 = fragMaskGraphic(_in, _GraphicTex5, _SceneGraphicWh5, _GraphicUvMinMax5, _GraphicUvMinMaxClip5, _GraphicUvMaskForScene5, _isGraphicSliced5, _GraphicRectSlicedBorder5, _GraphicUvSlicedBorderRatio5);
					color = (tex2D(_MainTex, _in.texcoord) + _TextureSampleAdd) * _in.color;
					color.a *= _graphicMainTex0.a*_graphicMainTex1.a*_graphicMainTex2.a*_graphicMainTex3.a*_graphicMainTex4.a*_graphicMainTex5.a;
				}				
                //裁剪
				color.a *= UnityGet2DClipping(_in.worldPosition.xy, _ClipRect);
				#ifdef UNITY_UI_ALPHACLIP
				clip (color.a - 0.001);
				#endif            
				return color;
			}
		ENDCG
		}
	}
}
