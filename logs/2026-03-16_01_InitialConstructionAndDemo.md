# 2026-03-16 実行ログ: InitialConstructionAndDemo

## 1. 概要
- **実施日**: 2026-03-16
- **目的**: WMSサンプルシステムの初期構築、代表的機能の実装、およびConsoleDemoによる動作確認

## 2. 実施内容

### 手順
1. **リポジトリ初期化 (Phase A)**: `.gitignore`, `LICENSE`, `README.md` の作成。
2. **ドキュメント作成 (Phase B)**: `docs/` 配下に業務機能、システム構成、データモデル、オンライン/バッチ機能、レポート定義などの仕様書を作成。
3. **Mermaid図作成 (Phase C)**: システム概要、機能マップ、ER図、業務フロー図などを作成。
4. **コード実装 (Phase D)**:
    - `Wms.Domain`: エンティティ (Item, Stock, InboundReceipt, OutboundOrder, etc.) の定義。
    - `Wms.Application`: ユースケース (GetStock, RegisterInbound, RegisterOutboundOrder, etc.) とポート定義。
    - `Wms.Infrastructure`: インメモリリポジトリの実装。
    - `Wms.ConsoleDemo`: 動作確認用コンソールアプリケーションの実装。
5. **ConsoleDemoの実行**: 実装したユースケースを順次実行し、正常動作を確認。

### 実行コマンド
```bash
dotnet run --project src/Wms.ConsoleDemo
```

### 実行結果 (抜粋)
```text
=== WMS Console Demo: 在庫照会 / 入荷登録 / 出荷指示登録 / 日次在庫集計 / 棚卸差異レポート / 在庫一覧表出力 ===

投入済みマスタ: 商品 3 件 / 倉庫 2 件 / ロケーション 4 件 / 出荷先 2 件

[単一在庫照会]
条件: ITEM-001 / WH-01 / LOC-001
結果: ITEM-001 標準部品A / WH-01 / LOC-001 / 数量 100

[入荷登録]
結果: INB-0001 / ITEM-001 / WH-01 / LOC-001 / 入荷 20 / 更新後在庫 120
入荷実績件数: 1

[入荷後の在庫照会]
結果: ITEM-001 標準部品A / WH-01 / LOC-001 / 数量 120

[出荷指示登録]
結果: OUT-0001 / ITEM-001 / WH-01 / LOC-001 / 指示 15 / 出荷先 CUS-001 / 状態 Planned
出荷指示件数: 1

[出荷指示登録後の在庫照会]
結果: ITEM-001 標準部品A / WH-01 / LOC-001 / 数量 120

[在庫一覧表出力: TEXT]
行数: 4
商品コード | 商品名 | 倉庫コード | ロケーションコード | 在庫数量
ITEM-001 | 標準部品A | WH-01 | LOC-001 | 120
ITEM-001 | 標準部品A | WH-01 | LOC-002 | 50
ITEM-002 | 標準部品B | WH-01 | LOC-003 | 30
ITEM-003 | 完成品C | WH-02 | LOC-001 | 10

[在庫一覧表出力: CSV]
行数: 4
ItemCode,ItemName,WarehouseCode,LocationCode,Quantity
ITEM-001,標準部品A,WH-01,LOC-001,120
ITEM-001,標準部品A,WH-01,LOC-002,50
ITEM-002,標準部品B,WH-01,LOC-003,30
ITEM-003,完成品C,WH-02,LOC-001,10

[棚卸差異レポート]
棚卸日: 2026-03-16
レポート件数: 2
差異件数: 1
ログ: Inventory difference report generated for 2026-03-16. Report lines: 2. Difference lines: 1.
IC-0001  ITEM-001   標準部品A      WH-01 LOC-001 理論 120 実棚 118 差異  -2
IC-0002  ITEM-002   標準部品B      WH-01 LOC-003 理論  30 実棚  30 差異   0

[日次在庫集計]
実行日: 2026-03-16
件数: 4
ログ: Daily stock aggregation completed for 2026-03-16. Snapshot count: 4.
2026-03-16 ITEM-001   標準部品A      WH-01 LOC-001 数量 120
2026-03-16 ITEM-001   標準部品A      WH-01 LOC-002 数量 50
2026-03-16 ITEM-002   標準部品B      WH-01 LOC-003 数量 30
2026-03-16 ITEM-003   完成品C       WH-02 LOC-001 数量 10
保存済みスナップショット件数: 4

[存在しない在庫照会]
条件: ITEM-001 / WH-02 / LOC-999
結果: 在庫なし (0)

[在庫一覧]
ITEM-001   標準部品A      WH-01 LOC-001 数量 120
ITEM-001   標準部品A      WH-01 LOC-002 数量 50
ITEM-002   標準部品B      WH-01 LOC-003 数量 30
ITEM-003   完成品C       WH-02 LOC-001 数量 10
```

## 3. 結果・考察
- **システム構造**: 中規模WMSとしての基本構造（DDD、レイヤ化）が確立され、各コンポーネントが疎結合に設計されていることを確認した。
- **機能動作**: 在庫照会、入荷、出荷、棚卸、日次集計といった主要機能が正常に動作し、データの整合性が保たれていることを確認した。
- **COBOLマイグレーション研究**: ドキュメント（`docs/70_research-notes/CobolEmulationPoints.md` 等）により、COBOLシステムとの対比や模倣ポイントが明確化された。
- **デモ環境**: インメモリ実装により、データベース環境を構築することなく、即座に動作確認が可能な状態となっている。
- **修正対応**: `Program.cs` における変数のスコープ問題を修正し、シナリオ実行順序を要件通りに整備した。

## 4. 次のアクション
- リモートリポジトリへのプッシュ
- 今後の機能拡張（Phase 2以降）に向けた課題整理
