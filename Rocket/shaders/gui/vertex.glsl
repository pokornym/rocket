#version 330

layout(location = 0) in vec2 vPosition;
layout(location = 1) in vec2 vTexCoords;

uniform mat4 uTransform;
uniform vec2 uScale;

out vec2 fTexCoords; 

void main() {
	gl_Position = uTransform * vec4(uScale * vPosition, -1, 1);
	fTexCoords = vTexCoords;
}
