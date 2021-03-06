#!/usr/bin/env ruby
#
# PostprocessBuildPlayer
#   Tested on Ruby 1.8.7, Gem 1.3.6, and xcodeproj 0.3.0
#   Created by akisute (http://akisute.com)
#   Licensed under The MIT License: http://opensource.org/licenses/mit-license.php
#
require 'rubygems'
require 'pathname'
require 'xcoder'
require 'rexml/document'
include REXML

#
# Define utility functions
#
def add_sys_framework_to_project(proj, framework_names, option=:required)
    proj.targets.each do |target|
        if target.name == "Unity-iPhone-simulator" then
            next
        end
        framework_names.each { |framework_name|
            fwork = proj.frameworks_group.create_system_framework framework_name
            target.framework_build_phase do
                add_build_file fwork
            end
        }
    end
end

def append_to_start_of_file(projpath, fname, line)
    newfile = File.new(projpath + "/" + fname + ".temp", "w")
    newfile.puts line;

    oldfile = File.open(projpath +  "/" + fname, "r+")
    oldfile.each_line { |line| newfile.puts line}

    oldfile.close();
    newfile.close();

    File.delete(projpath +  "/" + fname);
    File.rename(projpath +  "/" + fname + ".temp", projpath +  "/" + fname);
end

def add_remote_notifications_to_plist(path, fname)
    doc = Document.new File.open(path +  "/" + fname, "r+")
    doc.elements.each("plist/dict") { |root|
        key = root.add_element "key"
        key.text = "UIBackgroundModes"
        array = root.add_element "array"
        string = array.add_element "string"
        string.text = "remote-notification"
    }
    formatter = REXML::Formatters::Default.new
    File.open(path + "/" + fname + ".temp", "w") do |result|
        formatter.write(doc, result)
    end

    File.delete(path +  "/" + fname);
    File.rename(path +  "/" + fname + ".temp", path +  "/" + fname);
end

#
# If it's not iOS we're building, skip this
#
target = ARGV[1]
if (target == "iPhone") then

    #
    # Define build directory path
    # -> Will be suppried as argv if run by Unity
    # -> Else, assume UNITY_PROJECT_ROOT/build is a build directory
    #
    buildpath = (ARGV[0]) ? ARGV[0] : File.expand_path(File.dirname($0)) + "/../../build"
    puts "PostprocessBuildPlayer running on build directory: " + buildpath

    #
    # Add System frameworks required to build
    #
    projpath = buildpath + "/Unity-iPhone.xcodeproj"
    project = Xcode.project(projpath)
    add_sys_framework_to_project(project, ["MapKit", "CoreTelephony", "CoreLocation", "SystemConfiguration", "QuartzCore"], :required)
    project.save!

    #
    # Fix .pbxproj file by adding a line to it's start
    #
    append_to_start_of_file(projpath, "/project.pbxproj", "// !$*UTF8*$!")

    #
    # Add BackgroundTask <remote-notifications /> to .plist file
    #
    add_remote_notifications_to_plist(buildpath, "Info.plist")
else 
    puts "Not doing anything in postprocess. Building for " + target + "..."
end
