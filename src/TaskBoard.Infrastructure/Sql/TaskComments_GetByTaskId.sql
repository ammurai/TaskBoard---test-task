SELECT tc.Id, tc.TaskItemId, tc.UserId,
       CONCAT(u.FirstName, ' ', u.LastName) AS UserName,
       tc.Content, tc.DateCreated
FROM dbo.TaskComments tc
INNER JOIN dbo.Users u ON u.Id = tc.UserId
WHERE tc.TaskItemId = @TaskItemId
ORDER BY tc.DateCreated DESC;
