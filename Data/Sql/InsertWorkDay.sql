--declare @payId int = 69;
--declare @day DateTime = Cast('20191015' as DateTime);

if not exists (select * from WorkedDay 
				where SalaryPay_Id = @payId
					and Cast(Date as Date) = Cast(@day as Date))

insert WorkedDay (SalaryPay_Id, Date)
values (@payId, @day)
