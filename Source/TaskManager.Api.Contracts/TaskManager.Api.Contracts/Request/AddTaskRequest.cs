using System;
using System.ComponentModel.DataAnnotations;


namespace TaskManager.Api.Contracts.Request
{
    public class AddTaskRequest
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
        [Range(minimum: 1, maximum: 3)]
        public int Priority { get; set; }

        /// <summary>
        /// TAsk Status
        /// </summary>
        [Range(minimum: 1, maximum:3)]
        public int Status { get; set; }
    }
}
