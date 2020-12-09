using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace CAEProject.Models
{

    //是否置頂
    public enum IsTop
    {
        是,
        否
    }

    public enum IsRequired
    {
        是,
        否
    }

    //廣告類型
    public enum AdCategory
    {
        Top廣告,
        文字型廣告,
        小圖示廣告
    }

    //狀態(通用)
    public enum Status
    {
        草稿,
        發行,
        下架
    }

    //專案服務狀態
    public enum ServiceStatus
    {
        草稿,
        已通知,
        以收件,
        處理中,
        完成,
        無法成案
    }

    //專案服務類型
    public enum ServiceType
    {
        個案服務,
        技術報價,
        模擬除錯,
        轉委託案
    }

    //產業類別
    public enum Industry
    {
        金屬基本業,
        金屬製品製造業,
        其他
    }

    //軟體需求
    public enum Demand
    {
        結構應力,
        鑄造,
        鍛造,
        擠型,
        沖壓,
        模具
    }

    //CAE軟體需求
    public enum CaeDemand
    {
        結構應力,
        鑄造,
        鍛造,
        擠型,
        沖壓,
        應力,
        焊接熱處理,
        熱傳,
        流力
    }

    //軟體類型
    public enum SoftwareType
    {
        收費,
        試用,
        國網
    }

    //產業類別
    public enum IndustryCategory
    {
        展覽,
        演討會,
        合作資源,
        聯盟訊息,
        車輛產業,
        鑄造產業,
        鍛造產業,
        成行產業
    }

    //常見問題類別
    public enum FaqStatus
    {
        帳號申請相關問題,
        會員問題,
        軟體應用問題,
        課程報名問題,
        其他,
        客製化模擬服務
    }

    //研討會類別
    public enum SeminarStatus
    {
        研討會,
        課程
    }

    //是否訂閱
    public enum Subscription
    {
        是,
        否
    }

    //培訓需求
    public enum Training
    {
        是,
        否
    }

    //活動單位
    public enum Handle
    {
        自辦,
        委辦
    }

    //性別
    public enum GenderType
    {
        男,
        女
    }

    //發票類別
    public enum UniformInvoice
    {
        二聯單,
        三聯單
    }

    //公司屬性
    public enum CompanyType
    {

    }

    public enum ApplicationStatus
    {
        審核中,
        已啟用,
        以停止
    }

    //會員等級(下拉)
    public enum MemberLevel
    {
        鑽石會員,
        白金會費,
        一般會員,
        學生會員
    }


}