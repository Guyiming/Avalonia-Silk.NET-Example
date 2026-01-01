#version 300 es
precision mediump float;

//#version 330 core
layout (location = 0) in vec3 vPos;
layout (location = 1) in vec4 vColor;

uniform float uBlue;

//the next shader will use this variable
out vec4 fColor;

void main()
{
    gl_Position = vec4(vPos, 1.0);
    vec2 rb_half = vColor.rb * 0.5;
    vec4 color = vec4(rb_half, uBlue, vColor.a);
    fColor = color;
}