﻿namespace Services.EnumsModel
{
    public enum ApiCodeEnum
    {
        /// <summary>
        /// 處理中
        /// </summary>
        Processing = 1,

        /// <summary>
        /// 執行成功
        /// </summary>
        Success = 2,

        /// <summary>
        /// 欄位格式錯誤
        /// </summary>
        InputError = 3,

        /// <summary>
        /// 單據重複
        /// </summary>
        DuplicatedOrder = 4,

        /// <summary>
        /// 單據不存在
        /// </summary>
        OrderNotExists = 5,

        /// <summary>
        /// 單據不可取消
        /// </summary>
        OrderCanNotBeCanceled = 6,

        /// <summary>
        /// 單據已取消
        /// </summary>
        OrderAlreadyCanceled = 7,

        /// <summary>
        /// 寫入錯誤 (新增失敗 / 派發失敗)
        /// </summary>
        InsertError = 8,

        /// <summary>
        /// 資料筆數超過限制
        /// </summary>
        RequestOverLimit = 9,

        /// <summary>
        /// 商品已有庫存
        /// </summary>
        ItemInStock = 10,

        /// <summary>
        /// 系統錯誤
        /// </summary>
        SystemError = 999,
    }
}
