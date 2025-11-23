-- Insert data for ASAFirewallStatus table
INSERT INTO ASAFirewallStatuses (ID, Name, Description, CommandInput, SortOrder, IsDeleted)
VALUES 
    (NEWID(), 'CPU Usage <80%', 'Display ASA software version and system information', 'CPU Usage', 1, 0),
    (NEWID(), 'Overall hardware health', 'Display interface status and configuration', 'Overall hardware health', 2, 0);