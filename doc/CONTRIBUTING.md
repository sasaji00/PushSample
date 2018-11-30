# 開発者向けルール

## 必須環境

- Visual Studio Enterprise
  - プラグイン
    - 静的コード解析ツール : [StyleCop](https://marketplace.visualstudio.com/items?itemName=ChrisDahlberg.StyleCop)
- Git


## 作業開始から作業終了までの流れ

1. [GitHubにIssueを作成する](https://git-ytqrf2vs.jp-east-1.paas.cloud.global.fujitsu.com/mobile-native/YummyRecord/issues)
1. ローカルのmasterを最新にする
    1. `$ git checkout master`
    1. `$ git pull origin master`
1. ユーザーストーリー用ブランチを作成する
      - `$ git checkout -b us/123`
1. ユーザーストーリーから派生するタスク用ブランチを作成する
    - フロントエンドの作業の場合はブランチ名の先頭に `f/` を付ける
      - `$ git checkout -b f/123`
    - バックエンドの作業の場合はブランチ名の先頭に `b/` を付ける
      - `$ git checkout -b b/123`
    - README.md や CONTRIBUTING.md の作業の場合は何もつけない
      - `$ git checkout -b doc/123`
1. 空のコミットを作成する
    - `$ git commit --allow-empty -m "create branch"`
1. 空のコミットをプッシュする
    - `$ git push origin x/123`
1. [タイトルのプレフィックスに[WIP]を付けてプルリクエストを作成する](https://git-ytqrf2vs.jp-east-1.paas.cloud.global.fujitsu.com/mobile-native/YummyRecord/pulls)
1. 作業内容をcommitする（コミットログにはissue番号を付加する）
    - `$ git commit -m "working #123"`
1. 作業内容をpushする
    - `$ git push origin issue123`
1. Pull Requestでマージできる状態であることを確認する
    - VSTSのCIが成功していること
    - masterにマージできる状態であること
1. Pull Requestのタイトルに付けた[WIP]を外しReviewersを設定する
1. レビュアーへmentionを送る
1. レビューの後に指摘内容を修正を行う
1. レビュアーへmentionを送る
1. ブランチがマージされ、Pull RequestがCloseされたことを確認する
1. ブランチを削除する
    - `$ git branch -D issue123`
    - `$ git push origin :issue123`
1. issue を close する
1. 作業完了


## バグレポートの書き方テンプレート

バグをissueに挙げる際には、以下の文章をコピペしてご使用ください。

```
<!--- ここにバグの概要を書く -->

## 期待する結果
<!--- 必須 -->

## 現在の状況
<!--- 必須 -->

## 考えられる解決策
<!-- 書けるのであれば記載 -->

## バグを再現するための手順
<!--- 必須 -->
1.
2.
3.
4.

## ログ / スタックトレース
<!-- 書けるのであれば記載 -->

## 詳細說明
<!-- 書けるのであれば記載 -->

## 解決策の提案
<!-- 書けるのであれば記載 -->

```

## プルリクエストレビュー指摘事項の書き方

### ✨ GOOD

いいコード、素晴らしいコード、難しいところを良く頑張ったコードを賛美したいときにつける。

- アイコン： ✨
- コード:  `:sparkles:`

### 🙏  MUST

このままレビューを通してしまうと間違いなくバグが発生するコードや、仕様の把握に誤解があり必ず修正して欲しいときにつける。

- アイコン:  🙏
- コード:  `:pray:`

### 🤔 THINK

最低限の機能は果たすコードだけれども実行速度が極端に遅かったり、記述が冗長すぎたり、可読性が低かったりするときにつける。もう少し検討してほしいときにつける。

- アイコン: 🤔
- コード: `:thinking:`

### 💬 INFO

修正必須ではないが、もっとこうした方が良さそうというアドバイスしたいときにつける。

- アイコン: 💬
- コード: `:speech_balloon:`

### :eyes: TRIVIAL

コメント内のTypoや重箱の隅を突くようなコメントを書くときに使う。基本的に修正する必要はない。直して欲しいときには、MUST（:pray:） と組み合わせて使う。

- アイコン:  :eyes:
- コード:  `:eyes:`

### 🗣 DISCUSSION

実装の意図が分からず議論したい場合に付ける。

- アイコン: 🗣
- コード `:speaking_head:`


# 執筆ルール

- 作業用issueを作成する
- 自身のユーザーストーリーのブランチ配下に作業用ブランチを作る
  - （ブランチ派生例）
    - master
      - us107
        - doc/issue107
        - doc/issue108
- 画像素材は `img/` 配下に配置する
  - 例：png, jpg, gif
- 画像素材の名前は章を表す名詞 + 連番で表現する
  - 例：（環境構築であれば）Environment-01.png, Environment-02.png
- 画像素材を作るために使用した素材は `assets` 配下に配置する。名前は画像素材とリンクさせる。
  - 例：pptx
