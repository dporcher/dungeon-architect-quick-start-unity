﻿using UnityEngine;
using System.Collections;

namespace JackRabbit {
	public class PlayerController : MonoBehaviour {
		public float maxSpeed = 5;
		public float attackMoveSpeedMultiplier = 0.1f;
		public float sprintMultiplier = 1.5f;
		public float movementSensitivity = 0.1f;

		bool facingRight = true;
		Rigidbody2D rigidBody2D;
		Animator animator;
		bool attacking = false;


		void Awake() {
			rigidBody2D = GetComponent<Rigidbody2D>();
			animator = GetComponent<Animator>();
		}

		void FixedUpdate () {
			float moveX = Input.GetAxis("Horizontal");
			float moveY = Input.GetAxis("Vertical");
			
			attacking = Input.GetButton("Fire1");
			animator.SetBool("Attack", attacking);

			var sprintPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
			var targetSpeed = maxSpeed;
			if (attacking) {
				targetSpeed *= attackMoveSpeedMultiplier;
			}
			else if (sprintPressed) {
				targetSpeed *= sprintMultiplier;
			}
			var direction = new Vector2(moveX, moveY);
			var directionLength = direction.magnitude;
			if (directionLength > 1) {
				direction /= directionLength;
			}

			var currentSpeed = rigidBody2D.velocity.magnitude;
			var desiredSpeed = Mathf.Lerp(currentSpeed, targetSpeed, movementSensitivity);

			rigidBody2D.velocity = direction * desiredSpeed;
			
			if (moveX > 0 && !facingRight) {
				Flip();
			} else if (moveX < 0 && facingRight) {
				Flip ();
			}

			animator.SetFloat("Speed", rigidBody2D.velocity.magnitude);
		}

		void Update() {
		}

		void Flip() {
			facingRight = !facingRight;
			var scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}
}
