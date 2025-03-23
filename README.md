# demoNetCore8Api 專案文件

## 1. 專案概述

**專案名稱**：demoNetCore8Api  
**類型**：Web API  
**.NET 版本**：.NET 8.0  
**架構**：三層式架構（Controller、Services、Repository、Infrastructure）  
**依賴注入（DI）**：使用 Autofac  
**身份驗證與授權**：JWT  
**日誌記錄機制**：Serilog  
**測試框架**：  
- **單元測試**：NUnit  
- **UI 測試**：Atata  

## 2. 系統架構

本專案採用三層式架構，將業務邏輯與數據訪問分離，提高可維護性與擴展性。

### 2.1 層次結構

- **Controller 層**：處理 HTTP 請求，調用服務層，返回 API 響應。
- **Service 層**：包含業務邏輯，與 Repository 層交互。
- **Repository 層**：負責數據訪問，與 Infrastructure 層交互。
- **Infrastructure 層**：管理數據庫連接、第三方服務等基礎設施。

### 2.2 依賴注入（DI）與 IoC 容器

- 採用 **Autofac** 作為 IoC 容器。
- 透過 DI 註冊 Controller、Services、Repository，使組件之間的依賴關係鬆耦合。

## 3. 權限驗證機制

- 使用 **JWT（JSON Web Token）** 進行身份驗證。
- 用戶登入後，系統發放 JWT Token，後續請求需攜帶 Token 進行身份驗證。
- 支援角色授權，確保不同用戶的訪問權限。

## 4. 日誌管理

- 採用 **Serilog** 進行日誌管理。
- 支援多種日誌輸出（檔案、資料庫、ElasticSearch 等）。
- 記錄 API 訪問日誌、異常日誌、應用程式運行狀況等。

## 5. 測試策略

### 5.1 單元測試（NUnit）

- 採用 **NUnit** 作為單元測試框架。
- 透過 **Mocking（如 Moq）** 來隔離測試對象，確保測試的準確性。
- 主要針對 Service 層和 Repository 層的邏輯進行測試。

### 5.2 UI 測試（Atata）

- 使用 **Atata** 進行 UI 測試。
- 透過 **Selenium WebDriver** 驗證 Web API 的前端交互。
- 測試 API 端點的回應正確性與用戶行為模擬。

## 6. 開發環境與工具

- **IDE**：Visual Studio 2022 / Visual Studio Code  
- **版本控制**：Git（可使用 GitHub / GitLab / Azure DevOps）  
- **依賴管理**：NuGet  
- **數據庫**：SQL Server 
