--declare @payId int = 69;
--declare @year int = 2019;
--declare @month int = 1;
--declare @day int = 1;

delete WorkedDay 
where SalaryPay_Id = @payId
	and Year(Date) = @year
	and Month(Date) = @month
	and Day(Date) in (@day)
