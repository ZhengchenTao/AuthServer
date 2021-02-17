using System;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Entities.BaseEntities
{
    public class BaseEntity
    {
        #region Override Equals
        private static Guid IdEmpty => Guid.Empty;

        public override bool Equals(object obj)
        {
            if (Id == IdEmpty || obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Id.Equals((obj as BaseEntity).Id);
        }

        public override int GetHashCode()
        {
            if (Id == null)
            {
                return 0;
            }

            return Id.GetHashCode();
        }
        #endregion

        [Key]
        public Guid Id { get; set; }
    }
}
