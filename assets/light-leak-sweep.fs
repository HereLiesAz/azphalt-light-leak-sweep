/*{
  "DESCRIPTION": "A warm, organic light leak that drifts across the frame — the analog-film glow real editors reach for as a shortcut to a cinematic look.",
  "CATEGORIES": ["Guillotine", "Stylize"],
  "INPUTS": [
    { "NAME": "inputImage", "TYPE": "image" },
    { "NAME": "intensity", "TYPE": "float", "DEFAULT": 0.6, "MIN": 0.0, "MAX": 1.0 },
    { "NAME": "speed", "TYPE": "float", "DEFAULT": 0.15, "MIN": 0.02, "MAX": 0.6 }
  ]
}*/

void main() {
  vec2 uv = isf_FragNormCoord;
  vec4 c = IMG_THIS_PIXEL(inputImage);

  // A soft warm streak that drifts from one corner to the opposite, looping smoothly.
  float phase = fract(TIME * speed);
  vec2 leakCenter = mix(vec2(-0.3, 1.2), vec2(1.3, -0.2), phase);
  float dist = length(uv - leakCenter);
  float falloff = 1.0 - smoothstep(0.0, 0.9, dist);

  vec3 warmth = vec3(1.0, 0.72, 0.35);
  vec3 leaked = c.rgb + warmth * falloff * falloff * intensity;

  gl_FragColor = vec4(leaked, c.a);
}
