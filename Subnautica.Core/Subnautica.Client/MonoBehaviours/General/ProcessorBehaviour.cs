namespace Subnautica.Client.MonoBehaviours.General
{
    using Subnautica.Client.Abstracts;
    using System.Collections.Generic;
    using UnityEngine;

    public class ProcessorBehaviour : MonoBehaviour
    {
        private List<BaseProcessor> UpdateProcessors { get; set; } = new List<BaseProcessor>();

        private List<BaseProcessor> LateUpdateProcessors { get; set; } = new List<BaseProcessor>();

        private List<BaseProcessor> FixedUpdateProcessors { get; set; } = new List<BaseProcessor>();

        private List<BaseProcessor> DisposeProcessors { get; set; } = new List<BaseProcessor>();

        public void Update()
        {
            foreach (var processor in this.UpdateProcessors)
            {
                processor.OnUpdate();
            }
        }

        public void LateUpdate()
        {
            foreach (var processor in this.LateUpdateProcessors)
            {
                processor.OnLateUpdate();
            }
        }

        public void FixedUpdate()
        {
            foreach (var processor in this.FixedUpdateProcessors)
            {
                processor.OnFixedUpdate();
            }
        }

        public void OnDestroy()
        {
            foreach (var processor in this.DisposeProcessors)
            {
                processor.OnDispose();
            }
        }

        public void AddUpdateProcessor(BaseProcessor processors)
        {
            this.UpdateProcessors.Add(processors);
        }

        public void AddLateUpdateProcessor(BaseProcessor processors)
        {
            this.LateUpdateProcessors.Add(processors);
        }

        public void AddFixedUpdateProcessor(BaseProcessor processors)
        {
            this.FixedUpdateProcessors.Add(processors);
        }

        public void AddDisposeProcessor(BaseProcessor processors)
        {
            this.DisposeProcessors.Add(processors);
        }
    }
}
