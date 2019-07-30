Shader "RenderFX/Skybox Blended" {  
Properties {  
    _Tint ("Tint Color", Color) = (.5, .5, .5, .5)  
    
	_BlendMorning ("BlendMorning", Range(0.0,1.0)) = 0.5
	_BlendDay ("BlendDay", Range(0.0,1.0)) = 0.5
	_BlendSet ("BlendSet", Range(0.0,1.0)) = 0.5
	_BlendNight ("BlendNight", Range(0.0,1.0)) = 0.5  
    
	_FrontTexMorning ("FrontMorning (+Z)", 2D) = "white" {}  
    _BackTexMorning ("BackMorning (-Z)", 2D) = "white" {}  
    _LeftTexMorning ("LeftMorning (+X)", 2D) = "white" {}  
    _RightTexMorning ("RightMorning (-X)", 2D) = "white" {}  
    _UpTexMorning ("UpMorning (+Y)", 2D) = "white" {}  
    _DownTexMorning ("DownMorning (-Y)", 2D) = "white" {} 

	_FrontTexDay ("FrontDay (+Z)", 2D) = "white" {}  
    _BackTexDay ("BackDay (-Z)", 2D) = "white" {}  
    _LeftTexDay ("LeftDay (+X)", 2D) = "white" {}  
    _RightTexDay ("RightDay (-X)", 2D) = "white" {}  
    _UpTexDay ("UpDay (+Y)", 2D) = "white" {}  
    _DownTexDay ("DownDay (-Y)", 2D) = "white" {} 
	
	_FrontTexSet ("FrontSet (+Z)", 2D) = "white" {}  
    _BackTexSet ("BackSet (-Z)", 2D) = "white" {}  
    _LeftTexSet ("LeftSet (+X)", 2D) = "white" {}  
    _RightTexSet ("RightSet (-X)", 2D) = "white" {}  
    _UpTexSet ("UpSet (+Y)", 2D) = "white" {}  
    _DownTexSet ("DownSet (-Y)", 2D) = "white" {} 
    
	_FrontTexNight("FrontNight (+Z)", 2D) = "white" {}  
    _BackTexNight("BackNight (-Z)", 2D) = "white" {}  
    _LeftTexNight("LeftNight (+X)", 2D) = "white" {}  
    _RightTexNight("RightNight (-X)", 2D) = "white" {}  
    _UpTexNight("UpNight (+Y)", 2D) = "white" {}  
    _DownTexNight("DownNight (-Y)", 2D) = "white" {}  
}  
  
SubShader {  
    Tags { "Queue" = "Background" }  
    Cull Off  
    Fog { Mode Off }  
    Lighting Off  
    Color [_Tint]  
    Pass {  
        SetTexture [_FrontTexDay] { combine texture }  
        SetTexture [_FrontTexSet] { constantColor (0,0,0,[_BlendSet]) combine texture lerp(constant) previous }  
        SetTexture [_FrontTexSet] { combine previous +- primary, previous * primary }  
		SetTexture [_FrontTexNight] { constantColor (0,0,0,[_BlendNight]) combine texture lerp(constant) previous }
		SetTexture [_FrontTexMorning] { constantColor (0,0,0,[_BlendMorning]) combine texture lerp(constant) previous } 
		SetTexture [_FrontTexDay] { constantColor (0,0,0,[_BlendDay]) combine texture lerp(constant) previous } 
		}  	 	
    Pass {  
        SetTexture [_BackTexDay] { combine texture }  
        SetTexture [_BackTexSet] { constantColor (0,0,0,[_BlendSet]) combine texture lerp(constant) previous }  
        SetTexture [_BackTexSet] { combine previous +- primary, previous * primary }  
		SetTexture [_BackTexNight] { constantColor (0,0,0,[_BlendNight]) combine texture lerp(constant) previous }
		SetTexture [_BackTexMorning] { constantColor (0,0,0,[_BlendMorning]) combine texture lerp(constant) previous }
		SetTexture [_BackTexDay] { constantColor (0,0,0,[_BlendDay]) combine texture lerp(constant) previous } 
    }  
    Pass {  
        SetTexture [_LeftTexDay] { combine texture }  
        SetTexture [_LeftTexSet] { constantColor (0,0,0,[_BlendSet]) combine texture lerp(constant) previous }  
        SetTexture [_LeftTexSet] { combine previous +- primary, previous * primary }  
		SetTexture [_LeftTexNight] { constantColor (0,0,0,[_BlendNight]) combine texture lerp(constant) previous }
		SetTexture [_LeftTexMorning] { constantColor (0,0,0,[_BlendMorning]) combine texture lerp(constant) previous }
		SetTexture [_LeftTexDay] { constantColor (0,0,0,[_BlendDay]) combine texture lerp(constant) previous } 
		//SetTexture [_LeftTexNight] { combine previous +- primary, previous * primary }  
    }  
    Pass {  
        SetTexture [_RightTexDay] { combine texture }  
        SetTexture [_RightTexSet] { constantColor (0,0,0,[_BlendSet]) combine texture lerp(constant) previous }  
        SetTexture [_RightTexSet] { combine previous +- primary, previous * primary }  
		SetTexture [_RightTexNight] { constantColor (0,0,0,[_BlendNight]) combine texture lerp(constant) previous }
		SetTexture [_RightTexMorning] { constantColor (0,0,0,[_BlendMorning]) combine texture lerp(constant) previous }
		SetTexture [_RightTexDay] { constantColor (0,0,0,[_BlendDay]) combine texture lerp(constant) previous } 
		//SetTexture [_RightTexNight] { combine previous +- primary, previous * primary }  
    }  
    Pass {  
        SetTexture [_UpTexDay] { combine texture }  
        SetTexture [_UpTexSet] { constantColor (0,0,0,[_BlendSet]) combine texture lerp(constant) previous }  
        SetTexture [_UpTexSet] { combine previous +- primary, previous * primary }  
		SetTexture [_UpTexNight] { constantColor (0,0,0,[_BlendNight]) combine texture lerp(constant) previous }
		SetTexture [_UpTexMorning] { constantColor (0,0,0,[_BlendMorning]) combine texture lerp(constant) previous }
		SetTexture [_UpTexDay] { constantColor (0,0,0,[_BlendDay]) combine texture lerp(constant) previous } 
		//SetTexture [_UpTexNight] { combine previous +- primary, previous * primary }  
    }  
    Pass {  
        SetTexture [_DownTexDay] { combine texture }  
        SetTexture [_DownTexSet] { constantColor (0,0,0,[_BlendSet]) combine texture lerp(constant) previous }  
        SetTexture [_DownTexSet] { combine previous +- primary, previous * primary }  
		SetTexture [_DownTexNight] { constantColor (0,0,0,[_BlendNight]) combine texture lerp(constant) previous }
		SetTexture [_DownTexMorning] { constantColor (0,0,0,[_BlendMorning]) combine texture lerp(constant) previous }
		SetTexture [_DownTexDay] { constantColor (0,0,0,[_BlendDay]) combine texture lerp(constant) previous } 
		//SetTexture [_DownTexNight] { combine previous +- primary, previous * primary }  
    }  
}  
  
Fallback "RenderFX/Skybox", 1  
}  