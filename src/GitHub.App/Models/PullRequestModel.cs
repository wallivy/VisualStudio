﻿using System;
using System.Globalization;
using GitHub.Primitives;
using GitHub.VisualStudio.Helpers;
using NullGuard;
using System.Diagnostics;
using GitHub.SampleData;

namespace GitHub.Models
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class PullRequestModel : NotificationAwareObject, IPullRequestModel,
        IEquatable<PullRequestModel>,
        IComparable<PullRequestModel>
    {
        public PullRequestModel(int number, string title,
            IAccount author, [AllowNull]IAccount assignee,
            DateTimeOffset createdAt, DateTimeOffset? updatedAt = null)
        {
            Number = number;
            Title = title;
            Author = author;
            Assignee = assignee;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt ?? CreatedAt;
        }

        public void CopyFrom(IPullRequestModel other)
        {
            if (!Equals(other))
                throw new ArgumentException("Instance to copy from doesn't match this instance. this:(" + this + ") other:(" + other + ")", nameof(other));
            Title = other.Title;
            UpdatedAt = other.UpdatedAt;
            CommentCount = other.CommentCount;
            HasNewComments = other.HasNewComments;
            IsOpen = other.IsOpen;
            Assignee = other.Assignee;
        }

        public override bool Equals([AllowNull]object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            var other = obj as PullRequestModel;
            return other != null && Number == other.Number;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        bool IEquatable<IPullRequestModel>.Equals([AllowNull]IPullRequestModel other)
        {
            if (ReferenceEquals(this, other))
                return true;
            return other != null && Number == other.Number;
        }

        bool IEquatable<PullRequestModel>.Equals([AllowNull]PullRequestModel other)
        {
            if (ReferenceEquals(this, other))
                return true;
            return other != null && Number == other.Number;
        }

        public int CompareTo([AllowNull]IPullRequestModel other)
        {
            return other != null ? UpdatedAt.CompareTo(other.UpdatedAt) : 1;
        }

        public int CompareTo([AllowNull]PullRequestModel other)
        {
            return other != null ? UpdatedAt.CompareTo(other.UpdatedAt) : 1;
        }

        public static bool operator >([AllowNull]PullRequestModel lhs, [AllowNull]PullRequestModel rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return false;
            return lhs?.CompareTo(rhs) > 0;
        }

        public static bool operator <([AllowNull]PullRequestModel lhs, [AllowNull]PullRequestModel rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return false;
            return (object)lhs == null || lhs.CompareTo(rhs) < 0;
        }

        public static bool operator ==([AllowNull]PullRequestModel lhs, [AllowNull]PullRequestModel rhs)
        {
            return ReferenceEquals(lhs, rhs);
        }

        public static bool operator !=([AllowNull]PullRequestModel lhs, [AllowNull]PullRequestModel rhs)
        {
            return !(lhs == rhs);
        }

        public int Number { get; }

        string title;
        public string Title
        {
            get { return title; }
            set { title = value; this.RaisePropertyChange(); }
        }

        bool isOpen;
        public bool IsOpen
        {
            get { return isOpen; }
            set { isOpen = value; this.RaisePropertyChange(); }
        }

        int commentCount;
        public int CommentCount
        {
            get { return commentCount; }
            set { commentCount = value; this.RaisePropertyChange(); }
        }

        bool hasNewComments;
        public bool HasNewComments
        {
            get { return hasNewComments; }
            set { hasNewComments = value; this.RaisePropertyChange(); }
        }

        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public IAccount Author { get; set; }

        IAccount assignee;
        [AllowNull]
        public IAccount Assignee
        {
            [return: AllowNull]
            get { return assignee; }
            set { assignee = value; this.RaisePropertyChange(); }
        }


        [return: AllowNull] // nullguard thinks a string.Format can return null. sigh.
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "id:{0} title:{1} created:{2:u} updated:{3:u}", Number, Title, CreatedAt, UpdatedAt);
        }

        internal string DebuggerDisplay
        {
            get
            {
                return string.Format(CultureInfo.InvariantCulture, "id:{0} title:{1} created:{2:u} updated:{3:u}", Number, Title, CreatedAt, UpdatedAt);
            }
        }
    }
}
