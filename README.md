# wms-research-sample-repo

COBOL資産の移行設計・構造解析・保証空間研究のための、中規模WMS（Warehouse Management System）サンプルシステムの研究対象リポジトリです。

---

## リポジトリの目的

このリポジトリは、**本番業務システムではなく**、以下の目的を持つ研究用の具体例モデルです。

- 移行対象の実例を、公開可能な形で持つ
- 画面、帳票、バッチ、マスタ、トランザクション、在庫更新などの典型要素を含める
- COBOL的な構造を後から投入・模倣できるようにする
- AST/IR/CFG/DFG、責務境界、保証空間、移行難易度評価の実験対象とする
- 将来的に「現行業務構造」「移行設計」「再設計」の比較対象とする

---

## このサンプルが何の研究対象なのか

本リポジトリは、**移行設計・構造解析・保証空間研究のための、具体的で公開可能な中規模対象**を提供します。

研究対象としての位置づけは次の3層です。

| 層 | 内容 |
|----|------|
| 業務構造を見る層 | どの機能がどの責務を持つか、どのマスタ・トランザクションを扱うか |
| プログラム構造を見る層 | モジュール分割、依存方向、バッチとオンラインの分離、入出力境界 |
| 移行研究に使う層 | COBOL的責務に分割しやすい、再設計対象として論点化しやすい、AST/IR/保証単位の対応づけがしやすい |

---

## 想定するWMS業務範囲

### 中核機能

- 商品マスタ管理
- 倉庫マスタ管理
- ロケーション管理
- 在庫照会
- 入荷登録
- 出荷指示登録
- 引当処理
- 出荷実績確定
- 棚卸登録
- 棚卸差異確認
- 各種一覧・簡易帳票出力

### サブ機能

- 取引先マスタ
- 入荷予定データ
- 出荷予定データ
- 在庫移動
- ロット管理の簡易表現
- エラーログ・処理結果ログ

### バッチ処理の例

- 日次在庫集計
- 出荷実績集計
- 棚卸差異レポート作成
- 処理結果ログ出力

---

## ディレクトリ構成

```
wms-research-sample-repo/
├── README.md
├── .gitignore
├── LICENSE
├── docs/                    # 設計・仕様文書
│   ├── 00_project/          # プロジェクト概要
│   ├── 10_business-model/   # 業務モデル・機能一覧
│   ├── 20_system-structure/ # システム構成
│   ├── 30_data-model/       # データモデル
│   ├── 40_online/           # オンライン処理
│   ├── 50_batch/            # バッチ処理
│   ├── 60_reports/          # 帳票一覧
│   ├── 70_research-notes/   # 研究ノート
│   └── 80_future-work/      # 将来拡張計画
├── src/                     # ソースコード（最小限）
│   ├── Wms.Domain/
│   ├── Wms.Application/
│   ├── Wms.Infrastructure/
│   └── Wms.ConsoleDemo/
├── diagrams/                # 図表
├── log/                     # ログ出力
└── prompts/                 # プロンプト・手順
```

---

## 今後の拡張方針

段階的に拡張する計画です。

| Phase | 内容 |
|-------|------|
| Phase 1 | 骨格定義 |
| Phase 2 | 最小コード追加 |
| Phase 3 | 画面 / 帳票 / バッチ詳細化 |
| Phase 4 | COBOL的構造の模倣 |
| Phase 5 | 移行分析用注釈追加 |
| Phase 6 | 構造解析対象としての整備 |

---

## 注意事項

- **実在企業の業務ではありません**。汎用的なWMSの業務モデルです。
- **研究・解析・移行設計検証用のモデル**です。本番利用を想定していません。
- データ項目・帳票名・処理名は一般化しています。

---

## 今後の研究テーマ例

- 責務分割
- 画面-帳票-バッチ依存
- 在庫更新の保証単位
- 移行難易度
- 再設計候補抽出

---

## ライセンス

MIT License で公開しています。詳細は [LICENSE](LICENSE) を参照してください。

---

## English Summary

**wms-research-sample-repo** is a research-oriented sample repository for a mid-scale Warehouse Management System (WMS). It serves as a concrete, publicly available model for COBOL migration design, structural analysis, and assurance space research. The system is not intended for production use but provides typical elements such as screens, reports, batch jobs, masters, transactions, and inventory updates. The repository structure supports three research layers: business structure, program structure, and migration research. Planned extensions include phased development from skeleton definition through COBOL-like structure emulation to structural analysis tooling.
