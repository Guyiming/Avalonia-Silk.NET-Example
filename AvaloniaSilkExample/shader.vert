#version 330 core

layout (location = 0) in vec3 vPos;
layout (location = 1) in vec4 vColor;

//This is how we declare a uniform, they can be used in all our shaders and share the same name.
uniform float uBlue;

out vec4 fColor; //����������ͼ����ơ� ���������¸�fragment shader�л�ʹ�á�

void main()
{
    //gl_Position, is a built-in variable on all vertex shaders that will specify the position of our vertex.
    gl_Position = vec4(vPos, 1.0);

    //The rest of this code looks like plain old c (almost c#)
    vec4 color = vec4(vColor.rb / 2, uBlue, vColor.a); //Swizzling and constructors in glsl.
    fColor = color;
}