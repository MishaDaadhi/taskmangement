using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Business.Model
{
    public class TaskInfo
    {
        /// <summary>
        ///     Task unique identifier
        /// </summary>
        public Guid? Id { get; set; } = Guid.Empty;

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
        public TaskPriority Priority{get; set; }

        /// <summary>
        /// TAsk Status
        /// </summary>
        public Status Status { get; set; }
    }
}
