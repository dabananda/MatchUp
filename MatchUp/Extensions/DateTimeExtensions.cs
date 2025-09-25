namespace MatchUp.Extensions
{
    public static class DateTimeExtensions
    {
        public static int CalculateAge(this DateOnly? dob)
        {
            if (!dob.HasValue) return 0;
            var today = DateOnly.FromDateTime(DateTime.Now);
            var age = today.Year - dob.Value.Year;
            if (dob > today.AddYears(-age)) age--;
            return age;
        }
    }
}
