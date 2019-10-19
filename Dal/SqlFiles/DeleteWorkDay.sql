--DEBUG
--declare
--@payId int = 69,
--@day DateTime = Cast('20191015' as DateTime)

delete WorkedDay 
where SalaryPayId = @payId
	and Cast(Date as Date) = Cast(@day as Date)
