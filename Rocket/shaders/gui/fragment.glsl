#version 330

in vec2 fTexCoords;

uniform vec4 uColor;
uniform sampler2D uTexture;
uniform bool uTextureEnabled;

out vec4 color;

void main() {
	if (uTextureEnabled)
		color = texture(uTexture, fTexCoords) * uColor;
	else
		color = uColor;
}
