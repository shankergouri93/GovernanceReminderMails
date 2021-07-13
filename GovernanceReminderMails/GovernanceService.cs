using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GovernanceReminderMails
{
    public class GovernanceService
    {
        private Repository _repository;
        EmailService _emailService;
        public GovernanceService(Repository repository, EmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public async Task Run()
        {
            // var today = DateTime.Today;
            var today = new DateTime(2021, 07, 01);
            var smtps = await _repository.GetSMTPs(today);
            foreach (var smtp in smtps)
            {
                await _emailService.SendEmail(smtp);
            }
        }
    }
}
