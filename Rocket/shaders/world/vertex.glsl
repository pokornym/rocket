#version 330

struct LightData {
	vec4 position;
	vec3 color;
	float intensity;
};

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec3 vNormal;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;
uniform LightData uLight[8];
uniform int uLightCount;

out vec4 fPosition;
out vec4 fNormal;
out LightData fLight[8];

void main() {
	mat4 mv = uView * uModel;
	fPosition = mv * vec4(vPosition, 1);
	gl_Position = uProjection * fPosition;
	fNormal = transpose(inverse(mv)) * vec4(vNormal, 0.0);
	for (int i = 0; i < uLightCount; i++) {
		fLight[i].color = uLight[i].color;
		fLight[i].intensity = uLight[i].intensity;
		fLight[i].position = uView * uLight[i].position;
	}
}