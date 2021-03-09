using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D player_rb;

    enum RotateState {
        None,
        //Normal,
        Up,
        Down,
        Left,
        Right
    }
    float max_x_rotate = 9.0f;
    float min_x_rotate = 5.0f;
    //float normal_x_rotate = 7.0f;
    float max_y_rotate = 8.0f;
    float min_y_rotate = 2.0f;
    float rotate_speed = 2.25f;
    float y_offset = 2.0f;
    RotateState camera_vertical_state = RotateState.None;
    RotateState camera_horizontal_state = RotateState.None;
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + y_offset, transform.position.z);

        // rotate camera
        // vertical rotation
        if (player_rb.velocity.y > 0.01f || camera_vertical_state == RotateState.Up) {
            camera_vertical_state = RotateState.Up;
            if (this.transform.rotation.eulerAngles.x > max_x_rotate)
                camera_vertical_state = RotateState.None;
            else
                this.transform.Rotate(rotate_speed * Time.deltaTime, 0.0f, 0.0f);
        } else if (player_rb.velocity.y < 0.01f || camera_vertical_state == RotateState.Down) {
            camera_vertical_state = RotateState.Down;
            if (this.transform.rotation.eulerAngles.x < min_x_rotate)
                camera_vertical_state = RotateState.None;
            else
                this.transform.Rotate(-rotate_speed * Time.deltaTime, 0.0f, 0.0f);
        }
        // } else if (camera_state == RotateState.Normal) {
        //     if (this.transform.rotation.eulerAngles.x < normal_x_rotate)
        //         this.transform.Rotate(rotate_speed * Time.deltaTime, 0.0f, 0.0f);
        //     else if (this.transform.rotation.eulerAngles.x > normal_x_rotate)
        //         this.transform.Rotate(-rotate_speed * Time.deltaTime, 0.0f, 0.0f);
            
        //     if (Mathf.Abs(this.transform.rotation.eulerAngles.x - normal_x_rotate) < 0.5f)
        //         camera_state = RotateState.None;
        // }

        // if (camera_state == RotateState.None && Mathf.Abs(this.transform.rotation.eulerAngles.x - normal_x_rotate) > 0.5f)
        //     camera_state = RotateState.Normal;

        // horizontal rotation
        Debug.Log(player_rb.velocity.x);
        if (player_rb.velocity.x > 0.01f || camera_horizontal_state == RotateState.Right) {
            camera_horizontal_state = RotateState.Right;
            if (this.transform.rotation.eulerAngles.y > max_y_rotate)
                camera_horizontal_state = RotateState.None;
            else
                this.transform.Rotate(0.0f, rotate_speed * Time.deltaTime, 0.0f);
        } else if (player_rb.velocity.x < 0.01f || camera_horizontal_state == RotateState.Left) {
            camera_horizontal_state = RotateState.Left;
            if (this.transform.rotation.eulerAngles.y < min_y_rotate)
                camera_horizontal_state = RotateState.None;
            else
                this.transform.Rotate(0.0f, -rotate_speed * Time.deltaTime, 0.0f);
        }
    }
}
