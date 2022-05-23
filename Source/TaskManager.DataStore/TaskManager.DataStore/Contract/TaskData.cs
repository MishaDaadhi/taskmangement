using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManager.Core.DataStore;

namespace TaskManager.DataStore.Contract
{
    [Table("TaskData")]
    public class TaskData : BaseEntity
    {

        /// <summary>
        ///     Task title
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        ///     Task o	Description 
        /// </summary>
        [StringLength(50)]
        public string Description { get; set; }

        /// <summary>
        ///     Task DueDate
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        ///     Task DueDate
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        ///     Task DueDate
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Task Priority
        /// </summary>
        public string Priority { get; set; }

        /// <summary>
        /// TAsk Status
        /// </summary>
        public string Status { get; set; }

    }
}
