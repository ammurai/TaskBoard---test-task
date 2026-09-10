UPDATE dbo.Projects
SET IsArchived = @IsArchived,
    DateUpdated = SYSUTCDATETIME()
WHERE Id = @ProjectId;
