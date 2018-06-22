using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

            //docker images list 
            IList<ImagesListResponse> images = await client.Images.ListImagesAsync(new ImagesListParameters(), CancellationToken.None);
            var aa = images[0].Labels.TryGetValue("org.label-schema.schema-version", out string outstring);

            string continerid = "";


            ContainerStartParameters param = new ContainerStartParameters();
            

            await client.Containers.StartContainerAsync(continerid, param, CancellationToken.None);



            //컨테이너 종료
            //컨테이터 아이디 불러옴
            string stopcontiner = containers[0].ID;
            var stopped = await client.Containers.StopContainerAsync(stopcontiner, new ContainerStopParameters()
            {
                WaitBeforeKillSeconds = 10
            },
            CancellationToken.None);


            //docker image 를 docker hub에서 다운로드함.
            //임시로 내 테스트 업로드용 업데이트
            //string image = "p82468/kgy";
            //try
            //{
            //    var report = new Progress<JSONMessage>(msg =>
            //    {
            //        Console.WriteLine($"{msg.Status}|{msg.ProgressMessage}|{msg.ErrorMessage}");
            //    });

            //    // pull image again
            //    await client.Images.CreateImageAsync(new ImagesCreateParameters
            //    {
            //        FromImage = image
            //    },
            //    new AuthConfig(),
            //    report
            //    );

            //    Console.WriteLine($"{Environment.NewLine}Successfully pulled image {image} to {client.Configuration.EndpointBaseUri}");
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine($"Exception thrown attempting to pull image {image} to {client.Configuration.EndpointBaseUri}");
            //    Console.WriteLine($"{ex}");
            //}


        }

        private void button2_Click(object sender, EventArgs e)
        {
            DockerClient client = new DockerClientConfiguration(new Uri("http://192.168.0.25:4243")).CreateClient();




        }
    }
}
