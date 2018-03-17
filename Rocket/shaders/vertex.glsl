#version 330

layout(location = 0) in vec2 vPosition;
layout(location = 1) in vec4 vColor;
layout(location = 2) in vec2 vUV;

uniform mat4 uModel;
uniform mat4 uView;
uniform mat4 uProjection;
uniform float uLayer;

out vec4 fColor;
out vec2 fUV;

void main() {
	gl_Position = uProjection * uView * uModel * vec4(vPosition, 0, 1);
	fColor = vColor;
	fUV = vUV;
}
