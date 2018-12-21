﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace dushuhuiEntity
{

    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "书名")]
        public string BookName { get; set; }
        [Display(Name = "书图片")]
        public string Cover { get; set; }
        [Display(Name = "作者")]
        public string BookZuozhe { get; set; }
        [Display(Name = "译者")]
        public string BookYizhe { get; set; }
        [Display(Name = "出版社")]
        public string BookChubanshe { get; set; }
        [Display(Name = "ISBN")]
        public string BookISBN { get; set; }
        [Display(Name = "标签")]
        public string Tags { get; set; }
        [Display(Name = "局长推荐")]
        public string BookTuijian { get; set; }
        [Display(Name = "上线时间")]
        public DateTime CreateTime { get; set; }
    }

    public class Ren
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "头像")]
        public string RenAvatar { get; set; }
        [Display(Name = "姓名")]
        public string RenName { get; set; }
        [Display(Name = "性别")]
        public string RenSex { get; set; }
        [Display(Name = "生日")]
        public string RenBirthday { get; set; }
        [Display(Name = "一句话介绍")]
        public string RenYijuhua { get; set; }
        [Display(Name = "个人简介")]
        public string RenInfo { get; set; }
        [Display(Name = "权限")]
        public string RenQuanxian { get; set; }
         [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "邮箱格式不正确")]
        [Display(Name = "注册邮箱")]
        public string RenUserEmail { get; set; }
        [Display(Name = "密码")]
        public string RenPassword { get; set; }
        [Display(Name = "微信Openid")]
        public string RenOpenid { get; set; }
        [Display(Name = "微信Unionid")]
        public string RenUnionid { get; set; }
        [Display(Name = "手机号码")]
        public string RenPhone { get; set; }
        [Display(Name = "昵称")]
        public string RenNickName { get; set; }

        [Display(Name = "注册时间")]
        public DateTime CreateTime { get; set; }
        [Display(Name = "用户状态")]
        public bool Status { get; set; }
        [Display(Name = "陪读人状态")]
        public string PeiduStatus { get; set; }


    }

    public class Biji
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "笔记标题")]
        public string BijiTitle { get; set; }
        [Display(Name = "作者")]
        public int BijiZuozhe { get; set; }
        [Display(Name = "陪读人")]
        public int BijiRId { get; set; }
        [Display(Name = "书目")]
        public int BijiBId { get; set; }
        [Display(Name = "读书营")]
        public int BijiYId { get; set; }
        [Display(Name = "内容")]
        public string BijiContent { get; set; }
        [Display(Name = "打卡时间")]
        public string BijiTime { get; set; }
        [Display(Name = "最后修改时间")]
        public string BijiEditTime { get; set; }
        [Display(Name = "打卡类型")]
        public string BijiType { get; set; }
    }
    public class Ying
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "读书会名称")]
        public string YingTitle { get; set; }
         [Display(Name = "陪读人")]
        public int YingRId { get; set; }
         [Display(Name = "书目")]
        public int YingBId { get; set; }
         [Display(Name = "开始时间")]
        public string YingStartTime { get; set; }
         [Display(Name = "结束时间")]
        public string YingEndTime { get; set; }
         [Display(Name = "读书营介绍")]
         public string YingInfo { get; set; }
         [Display(Name = "读书营费用")]
         public float YingPay { get; set; }
    }
    public class YingList
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "参加人")]
        public int RId { get; set; }
        [Display(Name = "读书营")]
        public int YingId { get; set; }
        [Display(Name = "状态")]
        public string Status { get; set; }

    }

   

}
