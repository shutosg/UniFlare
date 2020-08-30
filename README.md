# UniFlare

UniFlare は、Unityのための拡張性のあるフレアライブラリです。
開発中の現在、UGUIの2D空間のみ対応しています。将来的に、MeshRenderer等を用いてUGUIに依存しない形で3D空間での描画や、ライトの追従、オブジェクトによる遮蔽等の対応も進めていく予定です。

![screenshot_01](https://user-images.githubusercontent.com/6266016/91664928-457b8500-eb2d-11ea-9089-6143d5538eb3.png)

## Demo
[こちら](http://sandbox.shutosg.net/uniflare_webgl/) のページからWebGL版のデモを試せます。

## Usage
[wiki](https://github.com/shutosg/UniFlare/wiki) にて随時解説ページを追加予定です。

## Installation

2つの方法でインストールできます。

### OpenUPM

[OpenUPM](https://openupm.com/) のCLIツールを用いてパッケージを追加できます

```shell
openupm add net.shutosg.uniflare
```

### manifest.json の編集

Unityプロジェクトの `Packages/manifest.json` に下記を直接追記してパッケージを追加できます。

※ `Packages/manifest.json` が存在しない場合、新たに作成する必要があります。

```json
{
  "dependencies": {
    "com.unity.ugui": "1.0.0",
    "net.shutosg.uniflare": "0.1.0",
    "com.unity.modules.imgui": "1.0.0"
  },
  "scopedRegistries": [
    {
      "name": "package.openupm.com",
      "url": "https://package.openupm.com",
      "scopes": [
        "com.openupm",
        "net.shutosg.uniflare"
      ]
    }
  ]
}

```

## License
MIT ライセンスです。