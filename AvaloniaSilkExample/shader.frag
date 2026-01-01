#version 300 es
precision mediump float;

//#version 330 core

//fColor come from last vertex shader output
in vec4 fColor;
  
out vec4 FragColor;

void main()
{
    FragColor = fColor;
}