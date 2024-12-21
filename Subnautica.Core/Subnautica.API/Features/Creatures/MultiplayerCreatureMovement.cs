namespace Subnautica.API.Features.Creatures
{
    using Subnautica.API.Features;
    using Subnautica.Network.Structures;

    using UnityEngine;

    public class MultiplayerCreatureMovement
    {
        public bool IsDriving = false;

        private float InterpolationTime = 0f;

        private Vector3 TargetPosition { get; set; }

        private Quaternion TargetRotation { get; set; }

        private MultiplayerCreature Creature { get; set; }

        private Vector3 Velocity;

        private Quaternion RotationVelocity;

        public MultiplayerCreatureMovement(MultiplayerCreature creature)
        {
            this.Creature = creature;
        }

        public void ResetCreature()
        {
            this.IsDriving = false;
            this.InterpolationTime = 0f;
            this.Creature.Locomotion.ResetUpDirectionOvverride();
        }

        public void SwimTo(Vector3 targetPosition, Quaternion targetRotation)
        {
            this.IsDriving         = true;
            this.TargetPosition    = targetPosition;
            this.TargetRotation    = targetRotation;
            this.InterpolationTime = (this.Creature.CreatureItem.Data.IsFastSyncActivated ? 0.1f : 0.2f) + 0.05f;
        }

        public bool SimpleMoveV2()
        {
            if (!this.IsDriving || !this.Creature.IsActive)
            {
                return false;
            }

            if (this.GetTargetDistance() > 750f || this.InterpolationTime <= 0.001f)
            {
                this.StopMovement();
            }
            else
            {
                this.Creature.GameObject.transform.position = Vector3.SmoothDamp(this.Creature.GameObject.transform.position, this.TargetPosition, ref this.Velocity, this.InterpolationTime);
                this.Creature.GameObject.transform.rotation = BroadcastInterval.QuaternionSmoothDamp(this.Creature.GameObject.transform.rotation, this.TargetRotation, ref this.RotationVelocity, this.InterpolationTime);

                this.Creature.Rigidbody.velocity = this.Velocity;
            }

            return true;
        }

        public bool SimpleMove()
        {
            if (!this.IsDriving || !this.Creature.IsActive)
            {
                return false;
            }

            if (this.GetTargetDistance() > 750f || this.InterpolationTime <= 0.001f)
            {
                this.StopMovement();
            }
            else
            {
                this.Creature.Rigidbody.velocity = this.GetVelocity();
            }

            this.InterpolationTime -= Time.fixedDeltaTime;

            return true;
        }

        public bool SimpleRotate()
        {
            if (!this.IsDriving || !this.Creature.IsActive)
            {
                return false;
            }

            this.Creature.Rigidbody.angularVelocity = Vector3.zero;

            return true;
        }

        private void StopMovement(bool movePosition = true)
        {
            this.Creature.Rigidbody.velocity        = Vector3.zero;
            this.Creature.Rigidbody.angularVelocity = Vector3.zero;

            if (movePosition)
            {
                this.Creature.Rigidbody.MovePosition(this.TargetPosition);
            }

            this.IsDriving         = false;
            this.InterpolationTime = 0f;
            this.TargetPosition    = Vector3.zero;
        }
        
        private float GetTargetDistance()
        {
            return ZeroVector3.Distance(this.TargetPosition, this.Creature.Rigidbody.transform.position);
        }

        private Vector3 GetVelocity()
        {
            return (this.TargetPosition - this.Creature.Rigidbody.transform.position) / this.InterpolationTime;
        }
    }
}