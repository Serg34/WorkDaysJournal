select b.*
	, (select COUNT(*) from BugIncident where Bug_Id = b.Id) IncidentCount
from Bug b
order by b.CreatedDate desc