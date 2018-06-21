using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Docker;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace dockerdotnetTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetcontinerList();
        }

        public async void GetcontinerList()
        {
            DockerClient client = new DockerClientConfiguration(new Uri("http://192.168.0.25:4243")).CreateClient();

            //ex) run 명령어 docker run -it --name "컨테이너 이름" centos:latest /bin/bash

            //실행 중인 컨테이너 리스트 출력
            IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(new ContainersListParameters() { Limit = 10 });




            //docker image 를 docker hub에서 다운로드함.
            var report = new Progress<JSONMessage>(msg =>
            {
                Console.WriteLine($"{msg.Status}|{msg.ProgressMessage}|{msg.ErrorMessage}");
            });

            // pull image again
            await client.Images.CreateImageAsync(new ImagesCreateParameters
            {
                FromImage = "p82468/kgy"
            },
            new AuthConfig(),
            report
            );


        }
    }
}
