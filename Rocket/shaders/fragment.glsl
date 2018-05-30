#version 330

struct Material {
	vec4 color;
	float ambient;
	float diffusion;
};

struct LightData {
	vec4 position;
	vec3 color;
	float intensity;
};

in vec4 fPosition;
in vec4 fNormal;
in LightData fLight[8];

uniform bool uShade;
uniform Material uMaterial;
uniform int uLightCount;

out vec4 color;

void main() {
	vec3 rgb = uMaterial.color.rgb;
	
	if (uShade) {
		for (int i = 0; i < uLightCount; i++) {
			if (fLight[i].intensity == 0)
				continue;
			vec4 l = fLight[i].position - fPosition;
      float diff = clamp(dot(fNormal, normalize(l)), 0, 1);
      rgb *= uMaterial.ambient + fLight[i].color * uMaterial.diffusion * diff / (length(l) / (fLight[i].intensity * 100));
		}
	}

	color = vec4(rgb, uMaterial.color.a);
}
