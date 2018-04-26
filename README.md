# numsieve
## 中国联通扫号器
###### C#写的扫号器，因为需求很简单所以直接用之前写的shodan-API工具改了改

###### url为拉取json的链接，可以直接用Fiddler抓取

###### 为避免封IP  采用单线程

### 更新日志：

* 20180426 内置了URL获取器，可快速检测json链接
* 20180424 分类功能基本完成（后续可能添加自定义规则），增加自定义防封延迟时间
* 20180420 增加部分分类(采用正则表达式P.S.持续过快刷新仍然会暂时封锁，过一会会好，后续版本会解决

### 近期目标：

* ~~添加号码分类~~
* 添加重复过滤
* ~~添加拉取间隔控制功能以避免被封~~
* ~~添加json获取工具~~
* 添加防封代理

### 界面

![pic](http://storage.iceagedata.com/github/numsieve180426_1.PNG)
![pic](http://storage.iceagedata.com/github/numsieve180426_2.PNG)
# 


# Release
* [Numsieve 1.1.2.2](https://github.com/KirosHan/numsieve/releases)
* ~~[Numsieve 1.1.2.1](https://github.com/KirosHan/numsieve/releases)~~
* ~~[Numsieve 1.0.3.1](https://github.com/KirosHan/numsieve/releases)~~
* ~~[Numsieve 1.0.2.1](https://github.com/KirosHan/numsieve/releases)~~
* ~~[Numsieve 1.0.1.1](https://github.com/KirosHan/numsieve/releases)~~
# 

### 使用方法
* Step1.进入URL获取器，勾选自动发现URL并点击开始，在内置浏览器中前往选号界面，程序会自动发现可用URL并复制
* Step2.回到程序主界面，将复制的URL填入对应输入框，点击开始即可开始扫号
![pic](http://storage.iceagedata.com/github/numsieve.gif)
![pic](https://github.com/KirosHan/numsieve/blob/master/web_resource/numsieve.gif)

### 打赏
支付宝![pic](https://github.com/KirosHan/numsieve/blob/master/web_resource/ali.jpg)
微信![pic](https://github.com/KirosHan/numsieve/blob/master/web_resource/wechat.jpg)
# License
MIT license. See [LICENSE](https://github.com/KirosHan/numsieve/blob/master/LICENSE)  for details.