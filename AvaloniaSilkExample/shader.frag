#version 330 core

in vec4 fColor;  //fColor�������������һ��vertex shader���������
  
out vec4 FragColor;

void main()
{
    FragColor = fColor;
}