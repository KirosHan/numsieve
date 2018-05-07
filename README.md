# numsieve
## 中国联通扫号器
###### C#写的扫号器，因为需求很简单所以直接用之前写的shodan-API工具改了改

###### url为拉取json的链接，可以直接用Fiddler或内置的地址获取器抓取

###### 为避免封IP  采用单线程

### 更新日志：

* 20180507 地址获取器3.0上线 （已删除1.0版本）
&ensp;&ensp;采用FiddlerCore内核，修复无法获取HTTPS链接的问题
* 20180502 地址获取器2.0测试中(建议使用地址获取器1.0报错的用户使用新版地址获取器)  
&ensp;&ensp;地址获取器摒弃1.0版本中内部嵌入浏览器的方式获取URL，采用开放式浏览器-监听网卡抓包，有效降低了内嵌浏览器带来的BUG
* 20180428 修复m.10010.com链接无法拉取数据bug
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

![pic](https://github.com/KirosHan/numsieve/blob/master/web_resource/numsieve180507_1.PNG)
![pic](https://github.com/KirosHan/numsieve/blob/master/web_resource/numsieve180507_2.PNG)
# 


# Release
* [Numsieve 1.2.3.12](https://github.com/KirosHan/numsieve/releases)
* ~~[Numsieve 1.1.4.15](https://github.com/KirosHan/numsieve/releases)~~
* ~~[Numsieve 1.1.3.2](https://github.com/KirosHan/numsieve/releases)~~
* ~~[Numsieve 1.1.2.2](https://github.com/KirosHan/numsieve/releases)~~
* ~~[Numsieve 1.1.2.1](https://github.com/KirosHan/numsieve/releases)~~
* ~~[Numsieve 1.0.3.1](https://github.com/KirosHan/numsieve/releases)~~
* ~~[Numsieve 1.0.2.1](https://github.com/KirosHan/numsieve/releases)~~
* ~~[Numsieve 1.0.1.1](https://github.com/KirosHan/numsieve/releases)~~
# 

### 使用方法
* Step1.进入URL获取器，勾选自动发现URL并点击开始，在浏览器中前往选号界面，程序会自动发现可用URL并复制
* Step2.回到程序主界面，将复制的URL填入对应输入框，点击开始即可开始扫号
![pic](https://github.com/KirosHan/numsieve/blob/master/web_resource/numsieve_180507.gif)

### 打赏
支付宝![pic](https://github.com/KirosHan/numsieve/blob/master/web_resource/ali.jpg)
微信![pic](https://github.com/KirosHan/numsieve/blob/master/web_resource/wechat.jpg)

### 反馈

QQ群 548825 点击链接加入群聊【ONBETA技术交流群】：[https://jq.qq.com/?_wv=1027&k=5L8HimB](https://jq.qq.com/?_wv=1027&k=5L8HimB)

# License
MIT license. See [LICENSE](https://github.com/KirosHan/numsieve/blob/master/LICENSE)  for details.