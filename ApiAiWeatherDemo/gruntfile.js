/// <binding BeforeBuild='bower:install, uglify:my_target' />
/*
This file in the main entry point for defining grunt tasks and using grunt plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkID=513275&clcid=0x409
*/
module.exports = function (grunt) {
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-watch');
	grunt.loadNpmTasks('grunt-bower-task');
	
    grunt.initConfig({
        uglify: {
            options: {
                sourceMap: true,
                sourceMapIncludeSources: true,
                mangle: false
            },
            my_target: {
                files: { 'Client/build/app.js': ['Client/**/*.js', '!Client/build/app.js'] }
            }
        },

        watch: {
            scripts: {
                files: ['Client/**/*.js', '!Client/build/app.js'],
                tasks: ['uglify']
            }
        },
		bower: {
			install: {
			   //just run 'grunt bower:install' and you'll see files from your Bower packages in lib directory 
			}
		}
    });

    grunt.registerTask('default', ['uglify', 'watch']);
};