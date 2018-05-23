#version 330

layout(location = 0) in vec3 vPosition;
layout(location = 1) in vec4 vColor;
layout(location = 2) in vec2 vUV;
layout(location = 3) in vec3 vNormal;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;

out vec4 fColor;
out vec2 fUV;
out vec4 fPosition;
out vec4 fNormal;

void main() {
	mat4 mv = uView * uModel;
	gl_Position = uProjection * mv * vec4(vPosition, 1);
	fColor = vColor;
	fUV = vUV;
	fPosition = mv * vec4(vPosition, 1);
	fNormal = transpose(inverse(mv)) * vec4(vNormal, 0.0);
}