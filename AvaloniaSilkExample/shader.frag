#version 330 core

in vec4 fColor;  //fColor这个变量来自上一个vertex shader的输出配置
  
out vec4 FragColor;

void main()
{
    FragColor = fColor;
}