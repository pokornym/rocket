#version 330

in vec4 fColor;
in vec2 fUV;
in vec4 fPosition;
in vec4 fNormal;

uniform bool uShade;

out vec4 color;

void main() {
	vec3 rgb = fColor.rgb;
	
	if (uShade) {
		vec4 camera = vec4(0.0, 0.0, 0.0, 1);
		vec4 l = camera - fPosition;
		float diff = clamp(dot(fNormal, l), 0, 1);
		rgb = (rgb * 0.1) + rgb * 250 * diff / length(l);
	}

	color = vec4(rgb, fColor.a);
}
