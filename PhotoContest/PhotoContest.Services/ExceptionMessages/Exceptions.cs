using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoContest.Services.ExceptionMessages
{
    public static class Exceptions
    {
        public const string InvalidCategory = "There is no such category.";
        public const string InvalidStatus = "There is no such status.";
        public const string InvalidContestName = "There is no contest with such name.";
        public const string InvalidContestID = "There is no contest with such ID.";
        public const string InvalidPhotoID = "There is no photo with such ID.";
        public const string InvalidUserID = "There is no user with such ID.";
        public const string InvalidUser = "There is no such user.";
        public const string InvalidFilter = "Filter is not valid.";
        public const string InvalidPhase = "Phase is not valid.";
        public const string InvalidSorting = "Sorting is not valid.";
        public const string InvalidDateTimePhase1 = "DateTime for Phase 1 is worng.";
        public const string InvalidDateTimePhase2 = "DateTime for Phase 2 is worng.";
        public const string InvalidDateTimeFinished = "DateTime for Finished phase is worng.";
        public const string DeletedCategory = "Category is deleted.";
        public const string RequiredContestName = "Contest name is required.";
        public const string RequiredPhotoName = "Photo name is required.";
        public const string RequiredPhotoDescription = "Photo description is required.";
        public const string RequiredPhotoURL = "Photo URL is required.";
        public const string RequiredUserID = "User ID is required.";
        public const string RequiredContestID = "Contest ID is required.";
        public const string RequiredRankID = "Rank ID is required.";
        public const string RequiredFirstName = "First name is required.";
        public const string RequiredLastName = "Last name is required.";
        public const string RequiredEmail = "Email is required.";
        public const string EnrolledUser = "User already enrolled in this contest.";
        public const string IncorrectPassword = "Incorrect password.";
        public const string IncorrectCredentials = "Incorrect credentials for user.";
        public const string ExistingEmail = "Email already exists.";
        public const string NotFoundEmail = "Email not found.";
        public const string NotFoundRole = "Role not found.";
    }
}
